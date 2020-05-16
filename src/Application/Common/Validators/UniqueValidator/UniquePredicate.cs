using System;
using System.Linq.Expressions;
using Dawn;
using FluentValidation.Validators;

namespace TagDossier.Application.Common.Validators.UniqueValidator
{
    public class UniquePredicate<TEntity> : IUniquePredicate<TEntity>
        where TEntity : class
    {
        private readonly IApplicationDbContext _db;
        private readonly ICurrentUserService _currentUserService;

        public UniquePredicate(IApplicationDbContext db, ICurrentUserService currentUserService)
        {
            Guard.Argument(db, nameof(db)).NotNull();
            Guard.Argument(currentUserService, nameof(currentUserService)).NotNull();

            _db = db;
            _currentUserService = currentUserService;
        }

        public IPropertyValidator WhereNot<T>(Func<T, Expression<Func<TEntity, bool>>> predicate)
            => new UniqueValidator<T, TEntity>(_db, _currentUserService, predicate);

        public IPropertyValidator WhereNot<T>(
            Func<T, object> propertyFinder,
            Func<TEntity, T, Expression<Func<TEntity, bool>>> predicate)
            => new UniqueWithInstanceValidator<T, TEntity>(_db, _currentUserService, propertyFinder, predicate);
    }
}