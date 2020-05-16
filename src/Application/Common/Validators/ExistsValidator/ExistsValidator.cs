using System.Threading;
using System.Threading.Tasks;
using Dawn;
using FluentValidation.Validators;
using TagDossier.Domain.Common;

namespace TagDossier.Application.Common.Validators.ExistsValidator
{
    public class ExistsValidator<TEntity> : AsyncValidatorBase
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;

        public ExistsValidator(IApplicationDbContext db, ICurrentUserService currentUserService)
            : base("Entity \"{EntityName}\" ({PropertyValue}) was not found.")
        {
            Guard.Argument(db, nameof(db)).NotNull();
            Guard.Argument(currentUserService, nameof(currentUserService)).NotNull();

            _db = db;
            _currentUserService = currentUserService;
        }

        protected override async Task<bool> IsValidAsync(PropertyValidatorContext context,
            CancellationToken cancellation)
        {
            var typeEntity = typeof(TEntity);
            context.MessageFormatter.AppendArgument("EntityName", typeEntity.Name);
            var entity = await _db.Set<TEntity>().GetAsync(context.PropertyValue, cancellation);
            if (entity is null)
            {
                return false;
            }

            if (typeof(IHaveAuditInfo).IsAssignableFrom(typeEntity) == false)
            {
                return true;
            }

            var auditableEntity = entity as IHaveAuditInfo;
            await _db.Entry(auditableEntity).Reference(x => x.Created.By).LoadAsync(cancellation);

            return auditableEntity.Created.By == _currentUserService.User;
        }
    }
}