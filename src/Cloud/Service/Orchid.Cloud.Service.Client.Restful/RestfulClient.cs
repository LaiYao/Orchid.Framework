using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using Orchid.Cloud.Service.Client;
using Orchid.Cloud.Service.Client.Abstractions;
using Orchid.Core.Utilities;
using System.Reflection;
using System.Text;

namespace Orchid.Cloud.Service.Client.Restful
{
    public class RestfulClient : IClient
    {
        #region | Fields |

        #endregion

        #region | Properties |

        public RestfulClientOptions Options { get; private set; }

        #endregion

        #region | Ctor |

        public RestfulClient(RestfulClientOptions options)
        {
            Check.NotNull(options, nameof(options));

            Options = options;
        }

        #endregion

        #region | Implements for IClient |

        public object CallService(IInvocation invocation, params object[] parameters)
        {
            var requestUri = new StringBuilder();

            var httpMethod = HttpMethod.GET;
            var httpMethodAttribute = invocation.Method.CustomAttributes.SingleOrDefault(_ => _.AttributeType == typeof(HttpMethodAttribute));
            if (httpMethodAttribute != null)
            {
                httpMethod = (HttpMethod)httpMethodAttribute.ConstructorArguments[0].Value;
            }

            var parameterInfoList = invocation.Method.GetParameters();
            List<ParameterInfo> urlParameters = new List<ParameterInfo>();
            List<ParameterInfo> bodyParameters = new List<ParameterInfo>();
            for (int i = 0; i < parameterInfoList.Length; i++)
            {
                var parameterInfo = parameterInfoList[i];
                if (parameterInfo.GetCustomAttribute(typeof(BodyAttribute)) != null)
                {
                    bodyParameters.Add(parameterInfo);
                }
                else
                {
                    // TODO: 复杂类型需要特殊处理
                    requestUri.Append($"{parameterInfo.Name}={parameters[i]} & ");
                }
            }

            var client = new HttpClient();

            var method = invocation.Method;

            if (method.Name == "TestMethod")
            {
                return (int)parameters[0] + (int)parameters[1];
            }
            else
            {
                //return 
            }

            return 0;
        }

        #endregion
    }
}
