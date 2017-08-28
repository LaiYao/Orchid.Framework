using System;
using System.Collections.Generic;
using System.Text;

namespace Orchid.Cloud.Service.Client.Abstractions
{
    /// <summary>
    /// 通讯及反序列化处理
    /// </summary>
    public interface IClient
    {
        object CallService(IInvocation invocation, params object[] parameters);
    }
}
