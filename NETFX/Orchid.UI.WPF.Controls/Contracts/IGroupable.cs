using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Orchid.SeedWork.Core;
using Orchid.SeedWork.Core.Contracts;

namespace Orchid.UI.WPF.Controls.Contracts
{
    public interface IGroupable : IIdentifiable<Guid>
    {
        IGroupable Parent { get; set; }
        bool IsGrouped { get; set; }
    }
}
