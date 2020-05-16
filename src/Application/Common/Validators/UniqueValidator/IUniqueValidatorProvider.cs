namespace TagDossier.Application.Common.Validators.UniqueValidator
{
    public interface IUniqueValidatorProvider
    {
        IUniquePredicate<TEntity> In<TEntity>() where TEntity : class; 
    }
}