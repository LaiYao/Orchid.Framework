using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Reflection.Emit;
using Orchid.Core.Utilities;
using System.Linq.Expressions;
using Orchid.Cloud.Service.Client.Abstractions;

namespace Orchid.Cloud.Service.Client
{
    public class Proxy<T>
    {
        #region | Fields |

        readonly List<IInvocation> _invocations = new List<IInvocation>();
        readonly object _invocationsLock = new object();

        readonly IClient _client;

        #endregion

        #region | Properties |

        public T Object { get; private set; }

        #endregion

        #region | Ctors |

        internal Proxy(IClient client)
        {
            Check.NotNull(client, nameof(client));
            _client = client;
        }

        #endregion

        public Proxy<T> Config(Expression<Action<T>> methodSelector, DefaultInvocationOptions invokeOptions)
        {
            var method = GetMethodFromExpression(methodSelector);
            // TODO: need to verify the method that from methodSelector
            if (!_invocations.Exists(_ => _.Method == method))
            {
                lock (_invocationsLock)
                {
                    if (!_invocations.Exists(_ => _.Method == method))
                    {
                        _invocations.Add(new DefaultInvocation(method, invokeOptions, _client));
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("The method can only be config once.");
            }

            return this;
        }

        public Proxy<T> Build()
        {
            var type = typeof(T);
            var typeInfo = typeof(T).GetTypeInfo();

            if (!typeInfo.IsInterface)
            {
                throw new InvalidOperationException($"The generaic parameter of ServiceClient can only be an interface.");
            }

            // Maybe we dont need to verify the interface is annoted with ServiceContractAttribute
            //if (!typeInfo.CustomAttributes.Any(_ => _.AttributeType == typeof(ServiceContractAttribute)))
            //{
            //    throw new InvalidOperationException($"The generaic parameter of ServiceClient should be a type with ServiceContractAttribute.");
            //}

            foreach (var method in type.GetMethods().Union(type.GetInterfaces().SelectMany(_ => _.GetMethods())))
            {
                if (!_invocations.Exists(_ => _.Method == method))
                {
                    lock (_invocationsLock)
                    {
                        if (!_invocations.Exists(_ => _.Method == method))
                        {
                            _invocations.Add(new DefaultInvocation(method, new DefaultInvocationOptions(), _client));
                        }
                    }
                }
            }

            Object = DynamicProxyFactory.CreateDynamicProxy<T>(_invocations);

            return this;
        }

        #region | Helpers |

        MethodInfo GetMethodFromExpression(LambdaExpression expression)
        {
            Check.NotNull(expression, nameof(expression));

            var callExpression = expression.Body as MethodCallExpression;
            if (callExpression == null)
            {
                throw new ArgumentException("The first parameter should be a method call.", nameof(callExpression));
            }

            return callExpression.Method;
        }

        #endregion
    }
}
