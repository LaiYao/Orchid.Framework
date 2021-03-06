﻿namespace Orchid.Core.Abstractions
{
    /// <summary>
    /// 表示一条记录可标识为“已删除”状态，亦可认为该记录被禁止使用
    /// </summary>
    public interface ISoftDeletable
    {
        bool IsDeleted { get; }
    }
}
