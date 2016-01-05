using Orchid.UI.WPF.Controls.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class DesignerCanvas : Canvas
    {
        #region | Fields |

        Point? _rubberbandSelectionStartPoint;

        #endregion

        #region | Properties |

        #region | CurrentSelection DP |

        public ISelectable CurrentSelection
        {
            get { return (ISelectable)GetValue(CurrentSelectionProperty); }
            set { SetValue(CurrentSelectionProperty, value); }
        }

        public static readonly DependencyProperty CurrentSelectionProperty =
        DependencyProperty.Register("CurrentSelection", typeof(ISelectable), typeof(DesignerCanvas));

        #endregion

        #region | Events |

        public event EventHandler<DragPathEventArgs> DragPathComplete;

        #endregion

        #endregion

        #region | Ctor |

        public DesignerCanvas()
        {

        }

        #endregion

        #region | Overrides |

        protected override Size MeasureOverride(Size constraint)
        {
            var result = new Size();

            foreach (UIElement item in Children)
            {
                var left = Canvas.GetLeft(item);
                var top = Canvas.GetTop(item);
                left = double.IsNaN(left) ? 0 : left;
                top = double.IsNaN(top) ? 0 : left;

                item.Measure(constraint);

                var desiredSize = item.DesiredSize;
                if (!double.IsNaN(desiredSize.Width) && !double.IsNaN(desiredSize.Height))
                {
                    result.Width = Math.Max(result.Width, left + desiredSize.Width);
                    result.Height = Math.Max(result.Height, top + desiredSize.Height);
                }
            }

            result.Width += 10;
            result.Height += 10;

            return result;
        }

        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Source == this)
            {
                _rubberbandSelectionStartPoint = new Point?(e.GetPosition(this));

                CurrentSelection = null;

                Focus();

                e.Handled = true;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed)
            {
                _rubberbandSelectionStartPoint = null;
            }

            if (_rubberbandSelectionStartPoint.HasValue)
            {
                var adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (adornerLayer != null)
                {
                    var adorner = new RubberbandAdorner(this, _rubberbandSelectionStartPoint);
                    adornerLayer.Add(adorner);
                }
            }

            e.Handled = true;
        }

        protected override void OnDrop(DragEventArgs e)
        {
            base.OnDrop(e);

            //var dragData=e.Data.GetData(typeof(DragObject)
        }

        #endregion

        #region | Methods |

        internal void RaiseDragPathCompleted(DragPathEventArgs eventArgs)
        {
            //激活事件，raise event
            if (DragPathComplete != null)
                DragPathComplete(this, eventArgs);
        }

        #endregion
    }

    public class DragPathEventArgs : EventArgs
    {
        public object Source { get; set; }

        public object Target { get; set; }

        public Point SourcePosition { get; set; }

        public Point TargetPosition { get; set; }
    }
}
