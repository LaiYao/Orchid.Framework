using Orchid.SeedWork.MVVM.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.MVVM
{
    [DataContract]
    public class NotifiableBase : INotifiable, INotificationShutable
    {
        #region | INotifyPropertyChanging |

        public event PropertyChangingEventHandler PropertyChanging;

        public void NotifyPropertyChanging<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!value.Equals(field))
            {
                field = value;
                NotifyPropertyChanging(propertyName);
            }
        }

        public void NotifyPropertyChanging([CallerMemberName] string propertyName = null)
        {
            if (IgnoreNotifyPropertyChanging)
                return;

            if (PropertyChanging != null)
            {
                PropertyChanging.Invoke(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanging(IList<string> propertyNames)
        {
            if (IgnoreNotifyPropertyChanging)
                return;

            if (propertyNames != null)
            {
                foreach (var item in propertyNames)
                {
                    NotifyPropertyChanging(item);
                }
            }
        }

        #endregion

        #region | INotifyPropertyChanged |

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (!value.Equals(field))
            {
                field = value;
                NotifyPropertyChanged(propertyName);
            }
        }

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (IgnoreNotifyPropertyChanged)
                return;

            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public void NotifyPropertyChanged(IList<string> propertyNames)
        {
            if (IgnoreNotifyPropertyChanged)
                return;

            if (propertyNames != null)
            {
                foreach (var item in propertyNames)
                {
                    NotifyPropertyChanged(item);
                }
            }
        }

        #endregion

        #region | INotificationShutable |

        [DefaultValue(false)]
        [Browsable(false)]
        [DataMember]
        public bool IgnoreNotifyPropertyChanging { get; set; }

        [DefaultValue(false)]
        [Browsable(false)]
        [DataMember]
        public bool IgnoreNotifyPropertyChanged { get; set; }

        #endregion

        #region | Helper Methods |

        protected void ApplyNewValue<TValue>(ref TValue oldValue, TValue newValue, [CallerMemberName] string propertyName = null)
        {
            if (oldValue == null && newValue == null) return;
            else if (oldValue != null && oldValue.Equals(newValue)) return;

            NotifyPropertyChanging(propertyName);
            oldValue = newValue;
            NotifyPropertyChanged(propertyName);
        }

        #endregion

        #region | Overrides |

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is NotifiableBase)) return false;

            if (Object.ReferenceEquals(this, obj)) return true;

            return IsEquals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static bool operator ==(NotifiableBase left, NotifiableBase right)
        {
            if (Object.Equals(left, null)) return (Object.Equals(right, null)) ? true : false;
            else return left.Equals(right);
        }

        public static bool operator !=(NotifiableBase left, NotifiableBase right)
        {
            return !(left == right);
        }

        public virtual bool IsEquals(object obj)
        {
            return Object.ReferenceEquals(this, obj);
        }

        #endregion
    }
}
