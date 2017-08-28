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

        readonly string _proxyTypeName;
        readonly Dictionary<MethodInfo, Expression> _mappers = new Dictionary<MethodInfo, Expression>();
        readonly Dictionary<MethodInfo, IInvokerExecutingFilter> _interceptors = new Dictionary<MethodInfo, IInvokerExecutingFilter>();

        readonly IInvoker _invoker;

        #endregion

        #region | Properties |

        public T Object { get; private set; }

        #endregion

        #region | Ctors |

        internal Proxy(IInvoker invoker)
        {
            //Check.NotNull(invoker, nameof(invoker));
            _invoker = invoker;
        }

        #endregion

        public Proxy<T> MapMethod<TResult>(Expression<Func<T, TResult>> methodSelector, LambdaExpression methodImplement)
        {
            var callExpression = methodSelector.Body as MethodCallExpression;
            MapMethod(callExpression, methodImplement);

            return this;
        }

        public Proxy<T> MapMethod(Expression<Action<T>> methodSelector, LambdaExpression methodImplement)
        {
            var callExpression = methodSelector.Body as MethodCallExpression;
            MapMethod(callExpression, methodImplement);

            return this;
        }

        public Proxy<T> AddInterceptor(Expression<Action<T>> methodSelector, IInvokerExecutingFilter tt)
        {

            return this;
        }

        public Proxy<T> Build()
        {
            Object = CreateActualProxy();
            return this;
        }

        #region | Helpers |

        T CreateActualProxy()
        {
            var type = typeof(T);
            var typeInfo = typeof(T).GetTypeInfo();

            if (!typeInfo.IsInterface)
            {
                throw new InvalidOperationException($"The generaic parameter of ServiceClient can only be an interface.");
            }

            if (typeInfo.IsGenericType)
            {
                throw new InvalidOperationException($"The generaic parameter of ServiceClient can not be an Generic type.");
            }

            if (typeInfo.CustomAttributes.Any(_ => _.AttributeType == typeof(ServiceContractAttribute)))
            {
                throw new InvalidOperationException($"The generaic parameter of ServiceClient should be a type with ServiceContractAttribute.");
            }

            foreach (var method in typeInfo.DeclaredMethods.Where(_ => _.IsPublic && !_.IsStatic))
            {
                if (!_mappers.ContainsKey(method))
                {
                    //method.in
                    //_mappers.Add(method,_invoker.)
                }
            }

            return (T)Activator.CreateInstance(type);
        }

        public static Type ExtractCorrectType(Type paramType, Dictionary<string, GenericTypeParameterBuilder> name2GenericType)
        {
            if (paramType.GetTypeInfo().IsArray)
            {
                int arrayRank = paramType.GetArrayRank();
                Type elementType = paramType.GetElementType();
                if (elementType.GetTypeInfo().IsGenericParameter)
                {
                    GenericTypeParameterBuilder genericTypeParameterBuilder;
                    if (!name2GenericType.TryGetValue(elementType.Name, out genericTypeParameterBuilder))
                    {
                        return paramType;
                    }
                    if (arrayRank == 1)
                    {
                        return genericTypeParameterBuilder.MakeArrayType();
                    }
                    return genericTypeParameterBuilder.MakeArrayType(arrayRank);
                }
                else
                {
                    if (arrayRank == 1)
                    {
                        return elementType.MakeArrayType();
                    }
                    return elementType.MakeArrayType(arrayRank);
                }
            }
            else
            {
                GenericTypeParameterBuilder genericTypeParameterBuilder2;
                if (paramType.GetTypeInfo().IsGenericParameter && name2GenericType.TryGetValue(paramType.Name, out genericTypeParameterBuilder2))
                {
                    return genericTypeParameterBuilder2.AsType();
                }
                return paramType;
            }
        }

        bool IsSignatureMatched(MethodInfo method, LambdaExpression expression)
        {
            //expression.Compile().
            return method.ReturnType == expression.ReturnType;
        }

        void MapMethod(MethodCallExpression methodSelector, Expression methodImplement)
        {
            Check.NotNull(methodSelector, nameof(methodSelector));
            Check.NotNull(methodImplement, nameof(methodImplement));

            if (methodSelector == null)
            {
                throw new ArgumentException("The first parameter should be a method call.", nameof(methodSelector));
            }

            if (_mappers.Keys.Contains(methodSelector.Method))
            {
                throw new ArgumentException("The method map can only be defined once.", nameof(methodSelector));
            }

            _mappers.Add(methodSelector.Method, methodImplement);
        }

        #endregion
    }
}
