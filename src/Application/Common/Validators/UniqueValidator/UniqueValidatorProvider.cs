namespace TagDossier.Application.Common.Validators.UniqueValidator
{
    public class UniqueValidatorProvider: IUniqueValidatorProvider
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;

        public UniqueValidatorProvider(IApplicationDbContext db, ICurrentUserService currentUserService)
            => (_db, _currentUserService) = (db, currentUserService);

        public IUniquePredicate<TEntity> In<TEntity>()
            where TEntity : class
            => new UniquePredicate<TEntity>(_db, _currentUserService);
    }
}