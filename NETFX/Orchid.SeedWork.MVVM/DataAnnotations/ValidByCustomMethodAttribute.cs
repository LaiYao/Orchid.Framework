using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Orchid.SeedWork.MVVM.DataAnnotations
{
    public class ValidByCustomMethodAttribute : ValidationBaseAttribute
    {
        string _validMethodName;
        MethodInfo _validMethod;

        public ValidByCustomMethodAttribute(string validMethodName)
        {
            if (validMethodName == null)
            {
                throw new ArgumentNullException("validMethod");
            }
            else
            {
                _validMethodName = validMethodName;
            }
        }

        public override string Valid(object parameter)
        {
            var result = string.Empty;

            var validObj = parameter as ValidatableBase;
            if (_validMethod == null)
            {
                _validMethod = validObj.GetType().GetMethod(_validMethodName,
                    BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            }

            if (_validMethod != null && _validMethod.ReturnType == typeof(string))
            {
                result = _validMethod.Invoke(validObj, null) as string;
            }

            return result;
        }
    }
}
