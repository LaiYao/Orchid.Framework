using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Orchid.SeedWork.MVVM.Contracts;
using Orchid.SeedWork.MVVM.DataAnnotations;
using System.ComponentModel;
using System.Runtime.Serialization;
using Orchid.SeedWork.Core.Validator;
using System.Collections;

namespace Orchid.SeedWork.MVVM
{
    [DataContract]
    public abstract class ValidatableBase : ReundoableBase, IValidator
    {
        #region | Properties |

        #region | ValidationProperties |

        private static Dictionary<Type, Dictionary<PropertyInfo, List<ValidationBaseAttribute>>> _ValidationProperties = null;
        public static Dictionary<Type, Dictionary<PropertyInfo, List<ValidationBaseAttribute>>> ValidationProperties
        {
            get
            {
                if (_ValidationProperties == null)
                {
                    _ValidationProperties = new Dictionary<Type, Dictionary<PropertyInfo, List<ValidationBaseAttribute>>>();
                }
                return _ValidationProperties;
            }
        }

        private static Dictionary<PropertyInfo, List<ValidationBaseAttribute>> GetValidationPropertiesViaType(Type type)
        {
            Dictionary<PropertyInfo, List<ValidationBaseAttribute>> result = new Dictionary<PropertyInfo, List<ValidationBaseAttribute>>();
            foreach (var property in type.GetProperties())
            {
                foreach (var attribute in property.GetCustomAttributes(false))
                {
                    if (attribute is ValidationBaseAttribute)
                    {
                        if (result.ContainsKey(property))
                        {
                            result[property].Add(attribute as ValidationBaseAttribute);
                        }
                        else
                        {
                            result.Add(property, new List<ValidationBaseAttribute>() { attribute as ValidationBaseAttribute });
                        }
                    }
                }
            }
            return result;
        }

        #endregion

        #endregion

        #region | Ctor |

        protected ValidatableBase()
        {
            var type = this.GetType();
            if (!ValidationProperties.Keys.Contains(type))
            {
                ValidationProperties.Add(type, GetValidationPropertiesViaType(type));
            }
        }

        #endregion

        #region | Member of IValidator |

        [Browsable(false)]
        public string Error
        {
            get { return null; }
        }

        [Browsable(false)]
        public string this[string columnName]
        {
            get { return ValidProperty(columnName); }
        }

        public string ValidProperty(string propertyName, string ruleSetName = "")
        {
            StringBuilder result = new StringBuilder();

            var property = ValidationProperties[GetType()].Keys.FirstOrDefault(dr => dr.Name == propertyName);

            if (property == null)
            {
                return string.Empty;
            }

            var attrList = ValidationProperties[GetType()][property];

            foreach (var item in attrList)
            {
                // we need pass the value of property to validate it
                var validResult = string.Empty;

                // verify via methods
                if (item is ValidByCustomMethodAttribute)
                {
                    var validByCustomMethodAttribute = item as ValidByCustomMethodAttribute;
                    validResult = validByCustomMethodAttribute.Valid(this);
                }
                else
                {
                    var propertyValue = property.GetValue(this, null);
                    validResult = item.Valid(propertyValue);
                }

                if (!string.IsNullOrEmpty(validResult))
                {
                    result.Append(validResult + "\r\n");
                }
            }

            return result.Length == 0 ? null : result.ToString().Remove(result.Length - 2);
        }

        [Browsable(false)]
        public bool HasErrors
        {
            get { return !GetValidationPropertiesViaType(this.GetType()).Keys.All(_ => ValidProperty(_.Name) == null); }
        }

        public IEnumerable GetErrors(string propertyName, string ruleSetName = "")
        {
            return ValidProperty(propertyName).Split(';').ToList();
        }

        #endregion

        //public string PreviewInputValidate(string path, object value)
        //{
        //    StringBuilder result = new StringBuilder();

        //    var dataContext = GetDataContext(path);

        //    if (dataContext != null && dataContext.Item1 != null && dataContext.Item2 != null)
        //    {
        //        var vb = dataContext.Item2 as ValidableBase;

        //        var property = ValidationProperties[vb.GetType()].FirstOrDefault(dr => dr.Key.Name == dataContext.Item1.Name);

        //        if (property.Key == null || property.Value == null) return null;

        //        var attrList = property.Value;

        //        foreach (var item in attrList)
        //        {
        //            // we need pass the value of property to validate it
        //            var validResult = string.Empty;

        //            // verify via methods
        //            if (item is ValidByCustomMethodAttribute)
        //            {
        //                var validByCustomMethodAttribute = item as ValidByCustomMethodAttribute;
        //                validResult = validByCustomMethodAttribute.Valid(this, value);
        //            }
        //            else
        //            {
        //                validResult = item.Valid(value);
        //            }

        //            if (!string.IsNullOrEmpty(validResult))
        //            {
        //                result.Append(validResult + "\r\n");
        //            }
        //        }

        //        return result.Length == 0 ? null : result.ToString().Remove(result.Length - 2);
        //    }

        //    return result.ToString();
        //}

        //public virtual double GetMinimum(string propertyPath)
        //{
        //    var result = double.NaN;

        //    var attrBound = GetBoundAttribute(propertyPath);
        //    if (attrBound != null)
        //    {
        //        result = attrBound.MinValue;
        //    }

        //    return result;
        //}

        //public virtual double GetMaximum(string propertyPath)
        //{
        //    var result = double.NaN;

        //    var attrBound = GetBoundAttribute(propertyPath);
        //    if (attrBound != null)
        //    {
        //        result = attrBound.MaxValue;
        //    }

        //    return result;
        //}

        //ValidBoundValueAttribute GetBoundAttribute(string propertyPath)
        //{
        //    ValidBoundValueAttribute result = null;
        //    var dataContext = GetDataContext(propertyPath);

        //    if (dataContext != null && dataContext.Item1 != null && dataContext.Item2 != null)
        //    {
        //        var vb = dataContext.Item2 as ValidableBase;

        //        var property = ValidationProperties[vb.GetType()].FirstOrDefault(dr => dr.Key.Name == dataContext.Item1.Name);

        //        if (property.Key == null || property.Value == null) return result;

        //        var attrList = property.Value;
        //        foreach (var item in attrList)
        //        {
        //            if (item is ValidBoundValueAttribute)
        //            {
        //                result = item as ValidBoundValueAttribute;
        //                break;
        //            }
        //        }
        //    }

        //    return result;
        //}

        //public Tuple<PropertyInfo, object> GetDataContext(string propertyPath)
        //{
        //    Tuple<PropertyInfo, object> result = null;

        //    if (!propertyPath.Contains("["))
        //    {
        //        var paths = propertyPath.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
        //        object data = this;
        //        var property = this.GetType().GetProperty(paths[0]);
        //        for (int i = 1; i < paths.Length; i++)
        //        {
        //            data = property.GetValue(data, null);
        //            property = property.PropertyType.GetProperty(paths[i]);
        //        }

        //        Debug.Assert(data is ValidableBase);

        //        result = new Tuple<PropertyInfo, object>(property, data);
        //    }

        //    return result;
        //}
    }
}
