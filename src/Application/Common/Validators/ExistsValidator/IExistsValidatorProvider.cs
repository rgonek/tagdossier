using FluentValidation.Validators;

namespace TagDossier.Domain.Common.Validators.ExistsValidator
{
    public interface IExistsValidatorProvider
    {
        IPropertyValidator In<TEntity>() where TEntity : class;
    }
}