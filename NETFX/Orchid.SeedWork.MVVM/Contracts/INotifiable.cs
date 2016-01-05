using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.MVVM.Contracts
{
    public interface INotifiable : INotifyPropertyChanged, INotifyPropertyChanging
    {
    }
}
