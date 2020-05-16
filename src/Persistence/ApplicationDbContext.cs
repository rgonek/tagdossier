using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Options;
using TagDossier.Application;
using TagDossier.Domain.Common;
using TagDossier.Domain.Entities;

namespace TagDossier.Persistence
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>,
        IApplicationDbContext
    {
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Dossier> Dossiers { get; set; }
        public DbSet<Connector> Connectors { get; set; }
        public DbSet<Source> Sources { get; set; }
        
        private IDbContextTransaction _currentTransaction;
        private readonly ICurrentUserService _currentUserService;

        public ApplicationDbContext(
            DbContextOptions options,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _currentUserService = currentUserService;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<IHaveAuditInfo>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                    {
                        entry.Entity.SetCreatedBy(_currentUserService.User);
                        Entry(entry.Entity.Created.By).State = EntityState.Unchanged;
                    }
                        break;
                    case EntityState.Modified:
                    {
                        entry.Entity.SetModifiedBy(_currentUserService.User);
                        Entry(entry.Entity.LastModified.By).State = EntityState.Unchanged;
                    }
                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                return;
            }

            _currentTransaction =
                await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted).ConfigureAwait(false);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync().ConfigureAwait(false);

                _currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                _currentTransaction?.Rollback();
            }
            finally
            {
                if (_currentTransaction != null)
                {
                    _currentTransaction.Dispose();
                    _currentTransaction = null;
                }
            }
        }
    }
}