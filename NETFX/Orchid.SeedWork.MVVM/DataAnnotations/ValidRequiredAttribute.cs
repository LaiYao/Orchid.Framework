using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.MVVM.DataAnnotations
{
    public class ValidRequiredAttribute:ValidationBaseAttribute
    {
        public override string Valid(object context)
        {
            return context==null?"必须项":null;
        }
    }
}
