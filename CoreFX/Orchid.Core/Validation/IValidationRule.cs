using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Core.Validation
{
    public interface IValidationRule<T>
    {
        string ErrorMessage { get; }

        bool Valid(T entity);
    }
}
