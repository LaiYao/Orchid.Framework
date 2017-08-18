namespace Orchid.Core.Validation
{
    public interface IValidationRule<T>
    {
        string ErrorMessage { get; }

        bool Valid(T entity);
    }
}
