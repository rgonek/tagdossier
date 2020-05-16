using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Dawn;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using TagDossier.Domain.Common;

namespace TagDossier.Application.Common.Validators.UniqueValidator
{
    public class UniqueValidator<T, TEntity> : AsyncValidatorBase
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;
        private readonly Func<T, Expression<Func<TEntity, bool>>> _predicate;

        public UniqueValidator(
            IApplicationDbContext db,
            ICurrentUserService currentUserService,
            Func<T, Expression<Func<TEntity, bool>>> predicate)
            : base("Entity \"{EntityName}\" ({PropertyValue}) already exists.")
        {
            Guard.Argument(db, nameof(db)).NotNull();
            Guard.Argument(currentUserService, nameof(currentUserService)).NotNull();
            Guard.Argument(predicate, nameof(predicate)).NotNull();

            _db = db;
            _currentUserService = currentUserService;
            _predicate = predicate;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var typeEntity = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", typeEntity.Name);

            var query = _db.Set<TEntity>().Where(_predicate((T)context.Instance));

            if (typeof(IHaveAuditInfo).IsAssignableFrom(typeEntity))
            {
                query = query
                    .OfType<IHaveAuditInfo>()
                    .Where(x => x.Created.By.Id == _currentUserService.User.Id)
                    .OfType<TEntity>();
            }

            return await query.AnyAsync(cancellation) == false;
        }
    }
}