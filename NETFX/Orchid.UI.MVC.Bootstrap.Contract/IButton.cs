﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.UI.MVC.Bootstrap.Contract
{
    public interface IButton<T>:IControlBase<T>
    {
        IButton<T> Text(string text);
    }
}
