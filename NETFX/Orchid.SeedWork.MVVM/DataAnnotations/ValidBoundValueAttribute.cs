using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchid.SeedWork.Core;

namespace Orchid.SeedWork.MVVM.DataAnnotations
{
    public class ValidBoundValueAttribute : ValidationBaseAttribute
    {
        ValidBoundOperation _operation;

        #region Properties

        double _MinValue;
        public double MinValue
        {
            get
            {
                return _MinValue;
            }
        }

        double _MaxValue;
        public double MaxValue
        {
            get
            {
                return _MaxValue;
            }
        }

        #endregion

        public ValidBoundValueAttribute(double minValue, double maxValue, ValidBoundOperation operation = ValidBoundOperation.EgtAndElt)
        {
            _MinValue = minValue;
            _MaxValue = maxValue;
            _operation = operation;
        }

        public override string Valid(object parameter)
        {
            var result = string.Empty;

            double value;
            if (parameter == null
                || !double.TryParse(parameter.ToString(), out value)
                || double.IsNaN(value)
                || double.IsInfinity(value))
            {
                throw new ArgumentException("expect parameter is a double value");
            }

            if ((_operation == ValidBoundOperation.EgtAndElt || _operation == ValidBoundOperation.EgtAndLt)
                && value < _MinValue)
            {
                // format
                result = LocalizationUtilities.GetLocalizedString("value can not less than") + " " + _MinValue;
            }

            if ((_operation == ValidBoundOperation.GtAndElt || _operation == ValidBoundOperation.GtAndLt)
                && value <= _MinValue)
            {
                result = LocalizationUtilities.GetLocalizedString("value must greater than") + " " + _MinValue;
            }

            if ((_operation == ValidBoundOperation.GtAndElt || _operation == ValidBoundOperation.EgtAndElt)
                && value > _MaxValue)
            {
                result = LocalizationUtilities.GetLocalizedString("value can not greater than") + " " + _MaxValue;
            }

            if ((_operation == ValidBoundOperation.GtAndLt || _operation == ValidBoundOperation.EgtAndLt)
                && value >= _MaxValue)
            {
                result = LocalizationUtilities.GetLocalizedString("value must less than") + " " + _MaxValue;
            }

            return result;
        }
    }

    [Flags]
    public enum ValidBoundOperation
    {
        /// <summary>
        /// (Minimum,Maximum)
        /// </summary>
        GtAndLt,
        /// <summary>
        /// [Minimum,Maximum)
        /// </summary>
        EgtAndLt,
        /// <summary>
        /// (Minimum,Maximum]
        /// </summary>
        GtAndElt,
        /// <summary>
        /// [Minimum,Maximum]
        /// </summary>
        EgtAndElt
    }
}
