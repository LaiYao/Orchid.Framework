using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchid.SeedWork.MVVM.DataAnnotations;
using Orchid.SeedWork.MVVM.Contracts;

namespace Orchid.SeedWork.MVVM
{
    public class DynamicEntity : ReundoableBase
    {
        #region | Fields |

        Dictionary<string, DynamicProperty> propertiesMap = new Dictionary<string, DynamicProperty>();

        #endregion

        #region | Properties |

        public dynamic this[string propertyName]
        {
            get
            {
                return propertiesMap[propertyName].PropertyValue;
            }
            set
            {
                if (value != propertiesMap[propertyName].PropertyValue)
                {
                    NotifyPropertyChanging("Item[]");
                    propertiesMap[propertyName].PropertyValue = value;
                    NotifyPropertyChanged("Item[]");
                }
            }
        }

        #endregion

        public void Add()
        {

        }

        void RePublishNotifyEvents(DynamicProperty propertyObject)
        {
            if (propertyObject != null)
            {
                propertyObject.PropertyChanging += (o, e) =>
                {
                    this.NotifyPropertyChanging("Item[]");
                };
                propertyObject.PropertyChanged += (o, e) =>
                {
                    this.NotifyPropertyChanged("Item[]");
                };
            }
        }

        #region | Members of IValidable |

        public string PreviewInputValidate(string propertyName, object value)
        {
            throw new NotImplementedException();
        }

        public double GetMinimum(string propertyName)
        {
            throw new NotImplementedException();
        }

        public double GetMaximum(string propertyName)
        {
            throw new NotImplementedException();
        }

        [field: NonSerialized]
        public event EventHandler<System.ComponentModel.DataErrorsChangedEventArgs> ErrorsChanged;

        public System.Collections.IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        public bool HasErrors
        {
            get { throw new NotImplementedException(); }
        }

        #endregion
    }

    public class DynamicProperty : ValidatableBase
    {
        #region | PropertyValue |

        dynamic _PropertyValue;
        [Reundoable]
        [ValidByCustomMethod("ValidPropertyValue")]
        public dynamic PropertyValue
        {
            get
            {
                return _PropertyValue;
            }
            set
            {
                if (value != _PropertyValue)
                {
                    NotifyPropertyChanging("PropertyValue");
                    _PropertyValue = value;
                    NotifyPropertyChanged("PropertyValue");
                }
            }
        }

        protected virtual string ValidPropertyValue(object value)
        {
            return string.Empty;
        }

        #endregion
    }
}
