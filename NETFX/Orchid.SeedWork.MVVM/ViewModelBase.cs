using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchid.SeedWork.MVVM.Contracts;
using Orchid.SeedWork.Core.Contracts;
using System.Runtime.Serialization;

namespace Orchid.SeedWork.MVVM
{
    [DataContract]
    public class ViewModelBase : NotifiableBase, INamable
    {
        #region | Members of INamable |

        #region | Name |

        private string _Name;
        [DataMember]
        public string Name
        {
            get { return _Name; }
            set
            {
                if (value == _Name)
                    return;
                NotifyPropertyChanging("Name");
                _Name = value;
                NotifyPropertyChanged("Name");
            }
        }

        #endregion

        #endregion
    }
}
