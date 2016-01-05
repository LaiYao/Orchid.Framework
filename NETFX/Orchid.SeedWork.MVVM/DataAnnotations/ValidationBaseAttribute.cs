using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orchid.SeedWork.MVVM.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class ValidationBaseAttribute : System.Attribute
    {
        public string RuleSet { get; set; }

        public virtual string Valid(object context)
        {
            return null;
        }

        public override string ToString()
        {
            return base.GetType().Name;
        }
    }
}
