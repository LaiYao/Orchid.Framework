namespace Orchid.Core.Abstractions
{
    /// <summary>
    /// Represent currrent life status of the object
    /// </summary>
    public interface ILifeTraceable
    {
        bool IsNew { get; set; }
        bool IsDirty { get; set; }
        bool IsDelete { get; set; }
    }
}
