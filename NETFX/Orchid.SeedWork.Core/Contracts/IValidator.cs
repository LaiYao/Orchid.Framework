using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections;

namespace Orchid.SeedWork.Core.Contracts
{
    public interface IValidator<T> : IDataErrorInfo where T : class
    {
        // 摘要: 
        //     获取一个值，该值指示实体是否包含验证错误。
        //
        // 返回结果: 
        //     如果该实体当前有验证错误，则为 true；否则为 false。
        bool HasErrors { get; }

        // 摘要: 
        //     获取针对指定属性或整个实体的验证错误。
        //
        // 参数: 
        //   propertyName:
        //     要检索其验证错误的属性的名称；或者为要检索实体级错误的 null 或 System.String.Empty。
        //
        // 返回结果: 
        //     针对属性或实体的验证错误。
        IEnumerable GetErrors(string propertyName, string ruleSetName = "");
    }
}
