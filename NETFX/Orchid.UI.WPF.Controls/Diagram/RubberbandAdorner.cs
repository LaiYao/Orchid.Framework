using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class RubberbandAdorner:Adorner
    {
        #region | Fields |

        Point? _startPoint;

        Point? _endPoint;

        Pen _pen;

        DesignerCanvas _canvas;

        #endregion

        #region | Properties |



        #endregion

        #region | Ctor |

        public RubberbandAdorner(DesignerCanvas canvas,Point? startPoint)
            :base(canvas)
        {
            _canvas = canvas;
            _startPoint = startPoint;

            _pen = new Pen(Brushes.LightSlateGray, 1);
            _pen.DashStyle = new DashStyle(new double[] { 2 }, 1);
        }

        #endregion

        #region | Overrides |



        #endregion

        #region | Methods |



        #endregion
    }
}
