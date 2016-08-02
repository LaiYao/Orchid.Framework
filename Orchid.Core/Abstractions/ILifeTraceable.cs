namespace Orchid.Core.Abstractions
{
    public class ILifeTraceable
    {
        bool IsNew { get; set; }
        bool IsDirty { get; set; }
        bool IsDelete { get; set; }
    }
}
