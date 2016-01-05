using Orchid.UI.WPF.Controls.Contracts;
using Orchid.Tool.UI.WPF;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Orchid.UI.WPF.Controls.Diagram
{
    [TemplatePart(Name = "Part_SourceArrow", Type = typeof(Grid))]
    [TemplatePart(Name = "Part_TargetArrow", Type = typeof(Grid))]
    public class ConnectingLineControl : Control, ISelectable, INotifyPropertyChanged
    {
        #region | Fields |

        ConnectingLineAdorner _adorner;

        Grid _SourceArrow;
        Grid _TargetArrow;

        #endregion

        #region | Properties |

        #region | IsSelected DP |

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register
        (
            "IsSelected",
            typeof(bool),
            typeof(ConnectingLineControl),
            new PropertyMetadata(false, new PropertyChangedCallback(IsSelectedPropertyChanged))
        );

        static void IsSelectedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region | SourceNode DP |

        public NodeControl SourceNode
        {
            get { return (NodeControl)GetValue(SourceNodeProperty); }
            set { SetValue(SourceNodeProperty, value); }
        }

        public static readonly DependencyProperty SourceNodeProperty =
        DependencyProperty.Register
        (
            "SourceNode",
            typeof(NodeControl),
            typeof(ConnectingLineControl),
            new PropertyMetadata(null, new PropertyChangedCallback(SourceNodePropertyChanged))
        );

        static void SourceNodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as ConnectingLineControl;

            var oldSourceNode = e.OldValue as NodeControl;
            var newSourceNode = e.NewValue as NodeControl;

            if (oldSourceNode != null)
            {
                oldSourceNode.PropertyChanged -= sd.SourcePoint_PropertyChanged;
                oldSourceNode.Lines.Remove(sd);
            }

            if (newSourceNode != null)
            {
                newSourceNode.Lines.Add(sd);
                newSourceNode.PropertyChanged += sd.SourcePoint_PropertyChanged;
            }

            sd.UpdatePathGeometry();
        }

        #endregion

        #region | SourceArrowSymbol |

        private ArrowSymbol _SourceArrowSymbol;
        public ArrowSymbol SourceArrowSymbol
        {
            get { return _SourceArrowSymbol; }
            set
            {
                ApplyNewValue(ref _SourceArrowSymbol, value);
            }
        }

        #endregion

        #region | SourceAnchorPosition DP |

        public Point SourceAnchorPosition
        {
            get { return (Point)GetValue(SourceAnchorPositionProperty); }
            set { SetValue(SourceAnchorPositionProperty, value); }
        }

        public static readonly DependencyProperty SourceAnchorPositionProperty =
        DependencyProperty.Register
        (
            "SourceAnchorPosition",
            typeof(Point),
            typeof(ConnectingLineControl),
            new PropertyMetadata(new Point(0, 0), new PropertyChangedCallback(SourceAnchorPositionPropertyChanged))
        );

        static void SourceAnchorPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as ConnectingLineControl;
            sd.UpdatePathGeometry();
        }

        #endregion

        #region | SourceAnchorAngle |

        private double _SourceAnchorAngle;
        public double SourceAnchorAngle
        {
            get { return _SourceAnchorAngle; }
            set
            {
                ApplyNewValue(ref _SourceAnchorAngle, value);
            }
        }

        #endregion

        #region | TargetNode DP |

        public NodeControl TargetNode
        {
            get { return (NodeControl)GetValue(TargetNodeProperty); }
            set { SetValue(TargetNodeProperty, value); }
        }

        public static readonly DependencyProperty TargetNodeProperty =
        DependencyProperty.Register
        (
            "TargetNode",
            typeof(NodeControl),
            typeof(ConnectingLineControl),
            new PropertyMetadata(null, new PropertyChangedCallback(TargetNodePropertyChanged))
        );

        static void TargetNodePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as ConnectingLineControl;

            var oldTargetNode = e.OldValue as NodeControl;
            var newTargetNode = e.NewValue as NodeControl;

            if (oldTargetNode != null)
            {
                oldTargetNode.PropertyChanged -= sd.TargetPoint_PropertyChanged;
                oldTargetNode.Lines.Remove(sd);
            }

            if (newTargetNode != null)
            {
                newTargetNode.Lines.Add(sd);
                newTargetNode.PropertyChanged += sd.TargetPoint_PropertyChanged;
            }

            sd.UpdatePathGeometry();
        }

        #endregion

        #region | TargetArrowSymbol |

        private ArrowSymbol _TargetArrowSymbol;
        public ArrowSymbol TargetArrowSymbol
        {
            get { return _TargetArrowSymbol; }
            set
            {
                ApplyNewValue(ref _TargetArrowSymbol, value);
            }
        }

        #endregion

        #region | TargetAnchorPosition DP |

        public Point TargetAnchorPosition
        {
            get { return (Point)GetValue(TargetAnchorPositionProperty); }
            set { SetValue(TargetAnchorPositionProperty, value); }
        }

        public static readonly DependencyProperty TargetAnchorPositionProperty =
        DependencyProperty.Register
        (
            "TargetAnchorPosition",
            typeof(Point),
            typeof(ConnectingLineControl),
            new PropertyMetadata(new Point(0, 0), new PropertyChangedCallback(TargetAnchorPositionPropertyChanged))
        );

        static void TargetAnchorPositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as ConnectingLineControl;
            sd.UpdatePathGeometry();
        }

        #endregion

        #region | TargetAnchorAngle |

        private double _TargetAnchorAngle;
        public double TargetAnchorAngle
        {
            get { return _TargetAnchorAngle; }
            set
            {
                ApplyNewValue(ref _TargetAnchorAngle, value);
            }
        }

        #endregion

        #region | PathGeometry |

        private PathGeometry _PathGeometry;
        public PathGeometry PathGeometry
        {
            get { return _PathGeometry; }
            set
            {
                ApplyNewValue(ref _PathGeometry, value);
            }
        }

        #endregion

        #region | LabelPosition DP |

        public Point LabelPosition
        {
            get { return (Point)GetValue(LabelPositionProperty); }
            set { SetValue(LabelPositionProperty, value); }
        }

        public static readonly DependencyProperty LabelPositionProperty =
        DependencyProperty.Register("LabelPosition", typeof(Point), typeof(ConnectingLineControl));

        #endregion

        #region | Label DP |

        public string Label
        {
            get { return (string)GetValue(LabelProperty); }
            set { SetValue(LabelProperty, value); }
        }

        public static readonly DependencyProperty LabelProperty =
        DependencyProperty.Register("Label", typeof(string), typeof(ConnectingLineControl));

        #endregion

        #region | StrokeDashArray |

        private DoubleCollection _StrokeDashArray;
        public DoubleCollection StrokeDashArray
        {
            get { return _StrokeDashArray; }
            set
            {
                ApplyNewValue(ref _StrokeDashArray, value);
            }
        }

        #endregion

        #endregion

        #region | Ctor |

        static ConnectingLineControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConnectingLineControl), new FrameworkPropertyMetadata(typeof(ConnectingLineControl)));
        }

        public ConnectingLineControl()
        {
            //SourcePoint = source;
            //TargetPoint = target;
            this.Unloaded += ConnectingLineControl_Unloaded;
        }

        void ConnectingLineControl_Unloaded(object sender, RoutedEventArgs e)
        {
            //SourcePoint = null;
            //TargetPoint = null;
        }

        #endregion

        #region | Overrides |

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _SourceArrow = this.Template.FindName("Part_SourceArrow", this) as Grid;
            _TargetArrow = this.Template.FindName("Part_TargetArrow", this) as Grid;
        }

        #endregion

        #region | INotifyPropertyChanged |

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        protected void ApplyNewValue<TValue>(ref TValue oldValue, TValue newValue, [CallerMemberName] string propertyName = null)
        {
            if (oldValue == null && newValue == null) return;
            else if (oldValue != null && oldValue.Equals(newValue)) return;

            oldValue = newValue;
            NotifyPropertyChanged(propertyName);
        }

        #endregion

        #region | Methods |

        void SourcePoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position")
            {
                UpdatePathGeometry();
            }
        }

        void TargetPoint_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Position")
            {
                UpdatePathGeometry();
            }
        }

        void ShowAdorner()
        {
            if (_adorner == null)
            {
                var designerCanvas = this.FindVisualAncestorByType<DesignerCanvas>();
                var adornerLayer = AdornerLayer.GetAdornerLayer(this);
                if (designerCanvas != null && adornerLayer != null)
                {
                    _adorner = new ConnectingLineAdorner(designerCanvas, this);
                    adornerLayer.Add(_adorner);
                }
            }

            _adorner.Visibility = Visibility.Visible;
        }

        void HideAdorner()
        {
            if (_adorner != null)
            {
                _adorner.Visibility = Visibility.Collapsed;
            }
        }

        void UpdatePathGeometry()
        {
            if (SourceNode != null
                &&
                TargetNode != null)
            {
                var pathStartPosition = new Point(SourceNode.Position.X + SourceAnchorPosition.X, SourceNode.Position.Y + SourceAnchorPosition.Y);
                var pathEndPosition = new Point(TargetNode.Position.X + TargetAnchorPosition.X, TargetNode.Position.Y + TargetAnchorPosition.Y);

                if (_SourceArrow != null)
                {
                    Canvas.SetLeft(_SourceArrow, pathStartPosition.X);
                    Canvas.SetTop(_SourceArrow, pathStartPosition.Y);
                }

                if (_TargetArrow != null)
                {
                    Canvas.SetLeft(_TargetArrow, pathEndPosition.X);
                    Canvas.SetTop(_TargetArrow, pathEndPosition.Y);
                }

                PathGeometry = PathFinder.GetPathGeometry(pathStartPosition, pathEndPosition);

                var labelX = Math.Min(pathStartPosition.X, pathEndPosition.X) + PathGeometry.Bounds.Width / 2;
                var labelY = Math.Min(pathStartPosition.Y, pathEndPosition.Y) + PathGeometry.Bounds.Height / 2;

                LabelPosition = new Point(labelX, labelY);

                // TODO: calculate the angle of relative arrow
                if (PathGeometry.Bounds.Width > PathGeometry.Bounds.Height)
                {
                    if (pathStartPosition.X < pathEndPosition.X)
                    {
                        TargetAnchorAngle = 0;
                    }
                    else
                    {
                        TargetAnchorAngle = 180;
                    }
                }
                else
                {
                    if (pathStartPosition.Y < pathEndPosition.Y)
                    {
                        TargetAnchorAngle = 90;
                    }
                    else
                    {
                        TargetAnchorAngle = 270;
                    }
                }
            }
        }

        #endregion
    }

    public enum ArrowSymbol
    {
        None,
        Arrow,
        Diamond
    }
}
