using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.MVVM.Contracts
{
    public interface IReundoable
    {
        string Name { get; set; }

        string Description { get; set; }

        object Undo(object parameter);

        object Redo(object parameter);
    }
}
