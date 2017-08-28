using System;
using System.Collections.Generic;
using System.Text;

namespace OMP.Domain.Entities
{
    public interface IHaveLables
    {
        Dictionary<string, string> Lables { get; }
    }
}
