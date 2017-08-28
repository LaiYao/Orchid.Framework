using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Orchid.Cloud.Service.Client.Abstractions;

namespace Orchid.Cloud.Service.Client
{
    public static class DynamicProxyFactory
    {
        #region | Fields |

        internal static readonly string DYNAMIC_ASSEMBLY_NAME = "Orchid.DynamicProxyGenAssembly";
        internal static readonly string PROXY_NAME_PERFIX = "__PROXY__";

        static readonly AssemblyBuilder _assemblyBuilder;
        static readonly ModuleBuilder _moduleBuilder;

        static readonly Dictionary<string, Type> _typeCache = new Dictionary<string, Type>();
        static readonly object _cacheLocker = new object();

        #endregion

        static DynamicProxyFactory()
        {
            _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName(DYNAMIC_ASSEMBLY_NAME), AssemblyBuilderAccess.Run);
            _moduleBuilder = _assemblyBuilder.GetDynamicModule(string.Empty);
        }

        public static T CreateDynamicProxy<T>(IEnumerable<IInvokerExecutingFilter> interceptors, string proxyTypeName = "")
        {
            var type = typeof(T);
            if (string.IsNullOrWhiteSpace(proxyTypeName)) proxyTypeName = PROXY_NAME_PERFIX + type.FullName;

            if (!_typeCache.Keys.Contains(proxyTypeName))
            {
                lock (_cacheLocker)
                {
                    if (!_typeCache.Keys.Contains(proxyTypeName))
                    {
                        _typeCache.Add(proxyTypeName, GenerateProxyType(type, proxyTypeName, interceptors));
                    }
                }
            }

            return (T)Activator.CreateInstance(_typeCache[proxyTypeName]);
        }

        #region | Helpers |

        static Type GenerateProxyType(Type type2Proxy, string proxyTypeName, IEnumerable<IInvokerExecutingFilter> interceptors)
        {
            var typeInfo = type2Proxy.GetTypeInfo();
            var parentType = typeInfo.IsInterface ? typeof(object) : type2Proxy;
            var typeBuilder = _moduleBuilder.DefineType(proxyTypeName, TypeAttributes.Class | TypeAttributes.Public | TypeAttributes.Sealed, parentType);

            if (typeInfo.IsInterface)
            {
                typeBuilder.AddInterfaceImplementation(type2Proxy);
            }

            typeBuilder.DefineField("_Interceptors", typeof(IEnumerable<IInvokerExecutingFilter>), FieldAttributes.Private);

            foreach (var method in typeInfo.DeclaredMethods.Where(_ => _.IsPublic && !_.IsStatic))
            {
                var interceptor = interceptors.SingleOrDefault(_ => _.Context.Method == method);
                if (interceptor != null)
                {
                    var argsType = method.GetParameters().Select(_ => _.ParameterType).ToArray();
                    var nestedTypeBuilder = GenerateMethodNestedClass(typeBuilder, method, argsType);
                    typeBuilder.DefineField(method.Name + "_field", nestedTypeBuilder.AsType(), FieldAttributes.Private);
                    // 定义回掉方法
                    if (!typeInfo.IsInterface)
                    {
                        var callbackMethodBuilder = typeBuilder.DefineMethod(method.Name + "_callback", MethodAttributes.Private, CallingConventions.Standard, method.ReturnType, argsType);
                        var ilGeneratorForCallback = callbackMethodBuilder.GetILGenerator();
                        ilGeneratorForCallback.Emit(OpCodes.Ldarg_0);
                        for (int i = 0; i < argsType.Length; i++)
                        {
                            ilGeneratorForCallback.Emit(OpCodes.Ldarg, i + 1);
                        }
                        ilGeneratorForCallback.Emit(OpCodes.Call, method);
                        ilGeneratorForCallback.Emit(OpCodes.Ret);
                    }
                    // 定义代理方法
                    var proxyMethodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Virtual, CallingConventions.Standard, method.ReturnType, argsType);
                    var ilGeneratorForProxyMethod = proxyMethodBuilder.GetILGenerator();
                    ilGeneratorForProxyMethod.Emit(OpCodes.Ldarg_0);
                }
            }

            return typeBuilder.AsType();
        }

        static TypeBuilder GenerateMethodNestedClass(TypeBuilder parentTypeBuilder, MethodInfo method, Type[] argsType)
        {
            var typebuilder = parentTypeBuilder.DefineNestedType("__" + method.Name + "__delegate", TypeAttributes.NestedPrivate | TypeAttributes.Sealed, typeof(MulticastDelegate));
            // Constructor
            var contructorBuilder = typebuilder.DefineConstructor(MethodAttributes.Public, CallingConventions.HasThis, new[] { typeof(object), typeof(IntPtr) });
            contructorBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);
            // Method -> Invoke
            var methodBuilder = typebuilder.DefineMethod("Invoke", MethodAttributes.Public, CallingConventions.Standard, method.ReturnType, argsType);
            methodBuilder.SetImplementationFlags(MethodImplAttributes.Runtime | MethodImplAttributes.Managed);

            return typebuilder;
        }

        static string GetUniqMethodName(MethodInfo methodInfo)
        {
            return methodInfo.Name + "_" + methodInfo.GetHashCode();
        }

        #endregion
    }
}
