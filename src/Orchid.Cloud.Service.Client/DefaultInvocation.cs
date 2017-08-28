using Orchid.Cloud.Service.Client.Abstractions;
using Orchid.Core.Utilities;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;

namespace Orchid.Cloud.Service.Client
{
    public class DefaultInvocation : IInvocation
    {
        public DefaultInvocationOptions Options { get; private set; }

        public IClient Client { get; private set; }

        public MethodInfo Method { get; private set; }

        public DefaultInvocation(MethodInfo method, DefaultInvocationOptions options, IClient client)
        {
            Check.NotNull(method, nameof(method));
            Check.NotNull(options, nameof(options));
            Check.NotNull(client, nameof(client));

            Method = method;
            Options = options;
            Client = client;
        }

        public object Invoke(object[] parameters)
        {
            object result = null;

            if (Options.ExecutingFilters != null)
            {
                foreach (var filter in Options.ExecutingFilters)
                {
                    filter.OnExecuting(this, parameters);
                }
            }

            switch (Options.FailureStrategy)
            {
                case FailureStrategy.Failover:
                    result = FailoverInvokeHandle(parameters);
                    break;
                case FailureStrategy.Failfast:
                    result = FailfastInvokeHandle(parameters);
                    break;
                case FailureStrategy.Failsafe:
                    result = FailsafeInvokeHandle(parameters);
                    break;
                case FailureStrategy.Failback:
                    result = FailbackInvokeHandle(parameters);
                    break;
                default:
                    break;
            }

            if (Options.ExecutedFilters != null)
            {
                foreach (var filter in Options.ExecutedFilters)
                {
                    filter.OnExecuted(this, parameters);
                }
            }

            return result;
        }

        #region | Helpers |

        object FailoverInvokeHandle(object[] parameters)
        {
            try
            {
                var exceptions = new List<Exception>();
                for (int i = 0; i < Options.RetryTimes + 1; i++)
                {
                    try
                    {
                        return ServiceCallWithTimeout(parameters);
                    }
                    catch (Exception ex)
                    {
                        exceptions.Add(ex);
                    }
                }

                if (exceptions.Count > 0) throw new AggregateException(exceptions);
            }
            catch (Exception ex)
            {
                if (Options.FailCallback != null)
                {
                    return Options.FailCallback.Invoke(parameters, ex);
                }
                else
                {
                    throw ex;
                }
            }

            return null;
        }

        object FailfastInvokeHandle(object[] parameters)
        {
            try
            {
                return ServiceCallWithTimeout(parameters);
            }
            catch (Exception ex)
            {
                if (Options.FailCallback != null)
                {
                    Options.FailCallback.Invoke(parameters, ex);
                }
                // 快速抛出异常，不返回FailCallback的执行结果
                throw ex;
            }
        }

        object FailsafeInvokeHandle(object[] parameters)
        {
            try
            {
                return ServiceCallWithTimeout(parameters);
            }
            catch (Exception ex)
            {
                if (Options.FailCallback != null)
                {
                    return Options.FailCallback.Invoke(parameters, ex);
                }
                // 忽略异常
                return null;
            }
        }

        object FailbackInvokeHandle(object[] parameters)
        {
            try
            {
                return ServiceCallWithTimeout(parameters);
            }
            catch (Exception ex)
            {
                if (Options.FailCallback != null)
                {
                    Options.FailCallback.Invoke(parameters, ex);
                }
                // 忽略异常
                return null;
            }
        }

        object ServiceCallWithTimeout(object[] parameters)
        {
            if (Options.Timeout == 0)
            {
                return Client.CallService(this, parameters);
            }
            else
            {
                var cts = new CancellationTokenSource();
                var taskResult = Task.Factory.StartNew(() => Client.CallService(this, parameters), cts.Token);
                taskResult.Wait(TimeSpan.FromSeconds(Options.Timeout));

                if (taskResult.IsCanceled)
                {
                    throw new TimeoutException();
                }
                else if (taskResult.IsFaulted)
                {
                    throw taskResult.Exception;
                }
                else if (taskResult.IsCompleted && !taskResult.IsFaulted)
                {
                    return taskResult.Result;
                }
            }

            return null;
        }

        #endregion
    }

    
}
