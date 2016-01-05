using System.Collections.Generic;
using System.Linq;
using Orchid.Core.Utilities;

namespace Orchid.Core.Validation
{
    public class ValidationResult
    {
        #region | Properties |

        public readonly List<string> Errors;

        public bool IsValid { get { return Errors.Count() == 0; } }

        #endregion

        public ValidationResult()
        {
            Errors = new List<string>();
        }

        public ValidationResult Add([NotNull]string error)
        {
            Check.NotEmpty(error, nameof(error));
            Errors.Add(error);
            return this;
        }

        public ValidationResult Add([NotNull]params string[] errors)
        {
            Check.NotEmpty(errors, nameof(errors));
            Errors.AddRange(errors);
            return this;
        }

        public ValidationResult Remove(string error)
        {
            Check.NotNull(error, nameof(error));
            Errors.Remove(error);
            return this;
        }
    }
}
