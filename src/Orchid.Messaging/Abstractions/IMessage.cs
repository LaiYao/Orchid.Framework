using System;
using Orchid.Core.Abstractions;

namespace Orchid.Messaging
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public interface IMessage<TKey> : IHasKey<TKey>
    {
        DateTime CreateTime { get; }
    }
}