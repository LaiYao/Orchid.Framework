using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Orchid.Core.Validation
{
    public interface ISelfValidation
    {
        ValidationResult ValidationResult { get; }

        bool IsValid { get; }
    }
}
