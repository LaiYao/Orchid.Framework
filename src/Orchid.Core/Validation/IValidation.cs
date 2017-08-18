namespace Orchid.Core.Validation
{
    public interface IValidator<T>
    {
        ValidationResult Valid(T entity);
    }
}
