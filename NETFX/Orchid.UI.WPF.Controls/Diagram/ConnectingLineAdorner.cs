using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class ConnectingLineAdorner : Adorner
    {
        #region | Fields |

        DesignerCanvas _canvas;

        ConnectingLineControl _line;

        Thumb _sourceDragThumb, _targetDragThumb;


        PathGeometry _path;

        VisualCollection _visuals;

        Pen _drawingPen;

        #endregion

        #region | Properties |



        #endregion

        #region | Ctor |

        public ConnectingLineAdorner(DesignerCanvas canvas, ConnectingLineControl line)
            : base(canvas)
        {
            _canvas = canvas;
            _line = line;

            this.Unloaded += ConnectingLineAdorner_Unloaded;
        }

        void ConnectingLineAdorner_Unloaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        #endregion
    }
}
