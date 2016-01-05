using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Orchid.Tool.UI.WPF;

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class DragThumb : Thumb
    {
        #region | Fields |



        #endregion

        #region | Properties |

        #region | Node |

        private NodeControl _Node;
        public NodeControl Node
        {
            get
            {
                if (_Node == null)
                {
                    _Node = this.FindVisualAncestorByType<NodeControl>();
                }
                return _Node;
            }
        }

        #endregion

        #region | Container |

        private DesignerCanvas _Container;
        public DesignerCanvas Container
        {
            get
            {
                if (_Container == null)
                {
                    _Container = this.FindVisualAncestorByType<DesignerCanvas>();
                }
                return _Container;
            }
        }

        #endregion

        #endregion

        #region | Ctor |

        static DragThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DragThumb), new FrameworkPropertyMetadata(typeof(DragThumb)));
        }

        public DragThumb()
        {
            DragDelta += DragThumb_DragDelta;
        }

        #endregion

        #region | Overrides |

        void DragThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Node != null && Container != null && Node.IsSelected)
            {
                var minLeft = double.MaxValue;
                var minTop = double.MaxValue;

                var item = Container.CurrentSelection as NodeControl;

                if (item != null)
                {
                    var left = Canvas.GetLeft(item);
                    var top = Canvas.GetTop(item);

                    minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
                    minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

                    double deltaHorizontal = Math.Max(-minLeft, e.HorizontalChange);
                    double deltaVertical = Math.Max(-minTop, e.VerticalChange);

                    if (double.IsNaN(left)) left = 0;
                    if (double.IsNaN(top)) top = 0;

                    Canvas.SetLeft(item, left + deltaHorizontal);
                    Canvas.SetTop(item, top + deltaVertical);
                }

                Container.InvalidateMeasure();
                e.Handled = true;
            }
        }

        #endregion

        #region | Methods |



        #endregion
    }
}
