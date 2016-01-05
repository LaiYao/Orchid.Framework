using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Orchid.UI.MVC.Bootstrap.Contract
{
    public interface IControlBase<T> : IHtmlString
    {
        IControlBase<T> Id(string id);

        IControlBase<T> Name(string name);

        IControlBase<T> Class(string @class);

        IControlBase<T> Data(IDictionary<string, object> data);

        IControlBase<T> HtmlAttribubtes(IDictionary<string, object> htmlAttributes);

        [EditorBrowsable(EditorBrowsableState.Never)]
        Type GetType();

        [EditorBrowsable(EditorBrowsableState.Never)]
        int GetHashCode();
    }
}
