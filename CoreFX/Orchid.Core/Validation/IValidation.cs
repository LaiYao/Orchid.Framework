using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Core.Validation
{
    public interface IValidation<T>
    {
        ValidationResult Valid(T entity);
    }
}
