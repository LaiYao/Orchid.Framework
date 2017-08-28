
namespace Orchid.Cloud.Service.Client.Abstractions
{
    public class IInvokerOptions
    {
        public int RetryTimes { get; set; }

        public FailStrategy FailStrategy { get; set; }

        public string EndpointIP { get; set; }

        public int EndpointPort { get; set; }
    }

    public enum FailStrategy
    {
        FAILOVER,
        FAILSAFE,
        FAILFAST,
        FAILBACK,
        FORKING
    }
}