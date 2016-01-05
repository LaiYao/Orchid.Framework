using Orchid.UI.MVC.Bootstrap.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.UI.MVC.Bootstrap.V3
{
    public class Button<T> : IButton<T>
    {
        #region | Properties |

        #region | Id |

        string _Id;
        public IButton<T> Id(string id)
        {
            _Id = id;

            return this;
        }

        #endregion

        #region | Text |

        string _Text;
        public Button<T> Text(string text)
        {
            _Text = text;
            return this;
        } 

        #endregion

        #region | Name |

        string _Name;
        public IButton<T> Name(string name)
        {
            _Name = name;

            return this;
        }
        
        #endregion

        #endregion

        

        public IButton<T> HtmlAttribubtes(IDictionary<string, object> htmlAttributes)
        {
            throw new NotImplementedException();
        }

        public string ToHtmlString()
        {
            throw new NotImplementedException();
        }
    }
}
