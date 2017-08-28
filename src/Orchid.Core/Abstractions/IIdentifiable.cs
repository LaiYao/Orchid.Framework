namespace Orchid.Core.Abstractions
{
    /// <summary>
    /// Use int as the default type of the Id property
    /// </summary>
    public interface IIdentifiable : IHasKey<int>
    {
    }
}
