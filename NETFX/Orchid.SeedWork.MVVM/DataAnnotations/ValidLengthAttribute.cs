using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.MVVM.DataAnnotations
{
    public class ValidLengthAttribute : ValidationBaseAttribute
    {
        int _minLength = -1;
        int _maxLength = -1;

        public ValidLengthAttribute(int minLength, int maxLength)
        {
            if (minLength < 0)
            {
                throw new ArgumentException("minLength cannot less than 0");
            }
            if (maxLength < 0)
            {
                throw new ArgumentException("maxLength cannot less than 0");
            }
            if (minLength >= maxLength)
            {
                throw new ArgumentException("minLength cannot greater than maxLength");
            }

            _minLength = minLength;
            _maxLength = maxLength;
        }

        public override string Valid(object parameter)
        {
            var result = string.Empty;

            if (parameter != null)
            {
                var context = parameter.ToString();

                if (_maxLength != -1 && context.Length > _maxLength)
                {
                    result = "Length can not greater than " + _maxLength;
                }
                if (_minLength != -1 && context.Length < _minLength)
                {
                    result = "Length can not less than " + _minLength;
                }
            }

            return result;
        }
    }
}
