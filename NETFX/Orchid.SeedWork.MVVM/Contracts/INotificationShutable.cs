using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.MVVM.Contracts
{
    public interface INotificationShutable
    {
        bool IgnoreNotifyPropertyChanging { get; set; }

        bool IgnoreNotifyPropertyChanged { get; set; }
    }
}
