namespace Orchid.Core.Abstractions
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}
