using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Orchid.Cloud.Service.Client.Abstractions;
using Orchid.Core.Utilities;

namespace Orchid.Cloud.Service.Client
{
    internal static class DynamicProxyFactory
    {
        #region | Fields |

        internal static readonly string DYNAMIC_ASSEMBLY_NAME = "Orchid.DynamicProxyGenAssembly";
        internal static readonly string PROXY_NAME_PERFIX = "__PROXY__";

        static readonly AssemblyBuilder _assemblyBuilder;
        static readonly ModuleBuilder _moduleBuilder;

        static readonly Dictionary<string, Type> _typeCache = new Dictionary<string, Type>();
        static readonly object _cacheLocker = new object();

        #endregion

        #region | Ctor |

        static DynamicProxyFactory()
        {
            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(DYNAMIC_ASSEMBLY_NAME), AssemblyBuilderAccess.Run);
            _moduleBuilder = _assemblyBuilder.DefineDynamicModule(DYNAMIC_ASSEMBLY_NAME);
        }

        #endregion

        internal static T CreateDynamicProxy<T>(IEnumerable<IInvocation> invocations, string proxyTypeName = "")
        {
            var type = typeof(T);
            if (string.IsNullOrWhiteSpace(proxyTypeName)) proxyTypeName = PROXY_NAME_PERFIX + type.FullName;

            if (!_typeCache.Keys.Contains(proxyTypeName))
            {
                lock (_cacheLocker)
                {
                    if (!_typeCache.Keys.Contains(proxyTypeName))
                    {
                        _typeCache.Add(proxyTypeName, GenerateProxyType(type, proxyTypeName, invocations));
                    }
                }
            }

            return (T)Activator.CreateInstance(_typeCache[proxyTypeName], invocations);
        }

        #region | Helpers |

        static Type GenerateProxyType(Type type2Proxy, string proxyTypeName, IEnumerable<IInvocation> invocations)
        {
            var typeInfo = type2Proxy.GetTypeInfo();
            if (!typeInfo.IsInterface)
            {
                throw new NotSupportedException("Now we only support to proxy interface.");
            }

            var parentType = typeInfo.IsInterface ? typeof(object) : type2Proxy;
            var typeBuilder = _moduleBuilder.DefineType(proxyTypeName, TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed, parentType);

            if (typeInfo.IsInterface)
            {
                typeBuilder.AddInterfaceImplementation(type2Proxy);
            }

            // 定义构造函数
            var constructorBuilder = typeBuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.Standard, new[] { typeof(IEnumerable<IInvocation>) });
            var ilGeneratorForConstructor = constructorBuilder.GetILGenerator();
            // Object.Ctor
            ilGeneratorForConstructor.Emit(OpCodes.Ldarg_0);
            ilGeneratorForConstructor.Emit(OpCodes.Call, typeof(object).GetConstructor(new Type[] { }));
            ilGeneratorForConstructor.Emit(OpCodes.Nop);
            ilGeneratorForConstructor.Emit(OpCodes.Nop);

            foreach (var invocation in invocations)
            {
                // 定义Invocation字段
                var invocationFieldName = InvocationExtensions.GetUniqueInvocationFieldName(invocation.Method);
                var invocationFieldBuilder = typeBuilder.DefineField(invocationFieldName, typeof(IInvocation), FieldAttributes.Private);
                // 构造函数初始字段赋值
                ilGeneratorForConstructor.Emit(OpCodes.Ldarg_0);
                ilGeneratorForConstructor.Emit(OpCodes.Ldarg_1);
                ilGeneratorForConstructor.Emit(OpCodes.Ldstr, invocationFieldName);
                ilGeneratorForConstructor.Emit(OpCodes.Call, typeof(InvocationExtensions).GetMethod("GetInvocationViaUniqueFieldName"));
                ilGeneratorForConstructor.Emit(OpCodes.Stfld, invocationFieldBuilder);

                // 定义代理方法实现
                var method = invocation.Method;
                var parametersType = method.GetParameters().Select(_ => _.ParameterType).ToArray();
                var proxyMethodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual, CallingConventions.Standard, method.ReturnType, parametersType);
                typeBuilder.DefineMethodOverride(proxyMethodBuilder, method);

                var ilGeneratorForProxyMethod = proxyMethodBuilder.GetILGenerator();
                ilGeneratorForProxyMethod.DeclareLocal(typeof(object));
                if (!method.ReturnType.Equals(typeof(void)))
                {
                    ilGeneratorForProxyMethod.DeclareLocal(method.ReturnType);
                }

                ilGeneratorForProxyMethod.Emit(OpCodes.Nop);
                ilGeneratorForProxyMethod.Emit(OpCodes.Ldarg_0);
                ilGeneratorForProxyMethod.Emit(OpCodes.Ldfld, invocationFieldBuilder);
                ilGeneratorForProxyMethod.Emit(OpCodes.Ldc_I4, parametersType.Length);
                ilGeneratorForProxyMethod.Emit(OpCodes.Newarr, typeof(object));
                for (int i = 0; i < parametersType.Length; i++)
                {
                    ilGeneratorForProxyMethod.Emit(OpCodes.Dup);
                    ilGeneratorForProxyMethod.Emit(OpCodes.Ldc_I4, i);
                    // TODO: 可能字符串参数需要特殊处理
                    ilGeneratorForProxyMethod.Emit(OpCodes.Ldarg, i + 1);
                    if (parametersType[i].GetTypeInfo().IsValueType)
                    {
                        ilGeneratorForProxyMethod.Emit(OpCodes.Box, parametersType[i]);
                    }
                    ilGeneratorForProxyMethod.Emit(OpCodes.Stelem_Ref);
                }

                ilGeneratorForProxyMethod.Emit(OpCodes.Callvirt, typeof(IInvocation).GetMethod("Invoke", new Type[] { typeof(object[]) }));
                ilGeneratorForProxyMethod.Emit(OpCodes.Stloc_0);

                if (!method.ReturnType.Equals(typeof(void)))
                {
                    ilGeneratorForProxyMethod.Emit(OpCodes.Ldloc_0);
                    if (!method.ReturnType.GetTypeInfo().IsValueType)
                    {
                        ilGeneratorForProxyMethod.Emit(OpCodes.Castclass, method.ReturnType);
                    }
                    else
                    {
                        ilGeneratorForProxyMethod.Emit(OpCodes.Unbox_Any, method.ReturnType);
                    }
                    ilGeneratorForProxyMethod.Emit(OpCodes.Stloc_1);
                    ilGeneratorForProxyMethod.Emit(OpCodes.Ldloc_1);
                }

                ilGeneratorForProxyMethod.Emit(OpCodes.Ret);
            }

            ilGeneratorForConstructor.Emit(OpCodes.Ret);

            return typeBuilder.CreateTypeInfo().AsType();
        }

        #endregion
    }
}
