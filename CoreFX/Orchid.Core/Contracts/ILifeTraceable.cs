namespace Orchid.Core.Contracts
{
    public class ILifeTraceable
    {
        bool IsNew { get; set; }
        bool IsDirty { get; set; }
        bool IsDelete { get; set; }
    }
}
