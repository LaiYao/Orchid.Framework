namespace Orchid.Core.Abstractions
{
    /// <summary>
    /// Represent the object can be identified by the Id property, and the type of the property is generic
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IHasKey<T>
    {
        T Id { get; }
    }
}
