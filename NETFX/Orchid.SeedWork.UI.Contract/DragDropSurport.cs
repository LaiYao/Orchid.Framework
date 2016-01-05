using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.UI.Contract
{
    public interface IDragSource
    {
        bool CanDrag(object ActualSource);
        //void Drag(object ActualSource);
    }

    public interface IDropTarget
    {
        bool CanDrop(object actualSource, object actualTarget);
        void Drop(object actualSource, object actualTarget,object tag);
    }
}
