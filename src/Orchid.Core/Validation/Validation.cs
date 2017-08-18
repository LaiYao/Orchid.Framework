using System.Collections.Generic;

namespace Orchid.Core.Validation
{
    public class Validator<TEnitty>
    {
        private readonly List<IValidationRule<TEnitty>> _validationRules=new List<IValidationRule<TEnitty>>();

        public Validator()
        {
            _validationRules = new List<IValidationRule<TEnitty>>();
        }

        protected virtual void AddRule(IValidationRule<TEnitty> rule) => _validationRules.Add(rule);
    }
}
