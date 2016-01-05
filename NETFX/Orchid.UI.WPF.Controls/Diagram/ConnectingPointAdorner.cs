using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class ConnectingPointAdorner : Adorner
    {
        #region | Fields |

        DesignerCanvas _canvas;

        ConnectingPointControl _startPoint;

        PathGeometry _pathGeometry;

        Pen _pen;

        #endregion

        #region | Properties |

        #region | HittedNode |

        private NodeControl _HittedNode;
        public NodeControl HittedNode
        {
            get { return _HittedNode; }
            set
            {
                if (value == _HittedNode)
                    return;
                _HittedNode = value;
            }
        }

        #endregion

        #region | HittedConnectingPoint |

        private ConnectingPointControl _HittedConnectingPoint;
        public ConnectingPointControl HittedConnectingPoint
        {
            get { return _HittedConnectingPoint; }
            set
            {
                if (value == _HittedConnectingPoint)
                    return;
                _HittedConnectingPoint = value;
            }
        }

        #endregion

        #endregion

        #region | Ctor |

        public ConnectingPointAdorner(DesignerCanvas canvas, ConnectingPointControl startPoint)
            : base(canvas)
        {
            _canvas = canvas;
            _startPoint = startPoint;

            _pen = new Pen(Brushes.LightSlateGray, 1);
            _pen.LineJoin = PenLineJoin.Round;
        }

        #endregion

        #region | Overrides |

        #region | Interaction |

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton == MouseButtonState.Pressed)
            {
                if (!IsMouseCaptured) CaptureMouse();
                HitTesting(e.GetPosition(this));
                // Update connecting line
                _pathGeometry = PathFinder.GetPathGeometry(_startPoint.TranslatePoint(new Point(_startPoint.ActualWidth / 2, _startPoint.ActualHeight / 2), _canvas), e.GetPosition(_canvas));
                InvalidateVisual();
            }
            else
            {
                if (IsMouseCaptured) ReleaseMouseCapture();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            if (HittedConnectingPoint != null)
            {
                var startPointPosition = _startPoint.TranslatePoint(new Point(_startPoint.ActualWidth / 2, _startPoint.ActualHeight / 2), _startPoint.ParentNode);
                var endPointPosition = HittedConnectingPoint.TranslatePoint(new Point(HittedConnectingPoint.ActualWidth / 2, HittedConnectingPoint.ActualHeight / 2), HittedNode);

                _canvas.RaiseDragPathCompleted(new DragPathEventArgs
                {
                    Source = _startPoint.ParentNode.DataContext,
                    Target = HittedNode.DataContext,
                    SourcePosition = new Point(startPointPosition.X - _startPoint.ParentNode.ActualWidth / 2, startPointPosition.Y - _startPoint.ParentNode.ActualHeight / 2),
                    TargetPosition = new Point(endPointPosition.X - HittedNode.ActualWidth / 2, endPointPosition.Y - HittedNode.ActualHeight / 2),
                });
                HittedNode.IsDragLineOver = false;
                //var line = new ConnectingLineControl(_startPoint, HittedConnectingPoint);
                //Canvas.SetZIndex(line, _canvas.Children.Count);
                // TODO: should to submit an event for adding connecting line
                //_canvas.Children.Add(line);
            }

            if (IsMouseCaptured) ReleaseMouseCapture();

            var adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);
            if (adornerLayer != null)
            {
                adornerLayer.Remove(this);
            }
        }

        #endregion

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            drawingContext.DrawGeometry(null, _pen, _pathGeometry);

            drawingContext.DrawRectangle(Brushes.Transparent, null, new Rect(RenderSize));
        }

        #endregion

        #region | Methods |

        void HitTesting(Point hitPoint)
        {
            bool hitConnectorFlag = false;

            DependencyObject hitObject = _canvas.InputHitTest(hitPoint) as DependencyObject;
            while (hitObject != null &&
                   hitObject != _startPoint.ParentNode &&
                   hitObject.GetType() != typeof(DesignerCanvas))
            {
                if (hitObject is ConnectingPointControl)
                {
                    HittedConnectingPoint = hitObject as ConnectingPointControl;
                    hitConnectorFlag = true;
                }

                if (hitObject is NodeControl)
                {
                    HittedNode = hitObject as NodeControl;
                    HittedNode.IsDragLineOver = true;
                    if (!hitConnectorFlag)
                        HittedConnectingPoint = null;
                    return;
                }
                hitObject = VisualTreeHelper.GetParent(hitObject);
            }

            HittedConnectingPoint = null;
            HittedNode = null;
        }

        #endregion
    }

    internal class PathFinder
    {
        public static PathGeometry GetPathGeometry(Point startPosition, Point endPosition)
        {
            PathGeometry pg = new PathGeometry();
            Point midP = new Point((startPosition.X + endPosition.X) / 2, (startPosition.X + endPosition.X) / 2);

            PathFigure pf = new PathFigure() { StartPoint = startPosition };
            BezierSegment bs = new BezierSegment()
            {
                Point1 = new Point(midP.X, startPosition.Y),
                Point2 = new Point(midP.X, endPosition.Y),
                Point3 = endPosition
            };
            pf.Segments.Add(bs);
            pg.Figures.Add(pf);

            return pg;
        }
    }
}
