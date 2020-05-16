namespace TagDossier.Domain.Common
{
    public interface IEntity<T>
    {
        T Id { get; }
    }
}