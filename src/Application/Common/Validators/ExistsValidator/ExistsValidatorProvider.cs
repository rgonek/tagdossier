using FluentValidation.Validators;
using TagDossier.Domain.Common.Validators.ExistsValidator;

namespace TagDossier.Application.Common.Validators.ExistsValidator
{
    public class ExistsValidatorProvider :IExistsValidatorProvider
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;

        public ExistsValidatorProvider(IApplicationDbContext db, ICurrentUserService currentUserService)
            => (_db, _currentUserService) = (db, currentUserService);

        public IPropertyValidator In<TEntity>()
            where TEntity : class
            => new ExistsValidator<TEntity>(_db, _currentUserService);
    }
}