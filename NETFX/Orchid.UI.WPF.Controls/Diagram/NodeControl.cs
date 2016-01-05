using Orchid.UI.WPF.Controls.Contracts;
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
using Orchid.Tool.UI.WPF;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Orchid.UI.WPF.Controls.Diagram
{
    [TemplatePart(Name = "Part_DragThumb", Type = typeof(DragThumb))]
    [TemplatePart(Name = "Part_ResizeDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "Part_PointDecorator", Type = typeof(Control))]
    [TemplatePart(Name = "Part_ContentPresenter", Type = typeof(ContentPresenter))]
    public class NodeControl : ContentControl, ISelectable, INotifyPropertyChanged
    {
        #region | Fields |



        #endregion

        #region | Properties |

        #region | IsSelected DP |

        public bool IsSelected
        {
            get { return (bool)GetValue(IsSelectedProperty); }
            set { SetValue(IsSelectedProperty, value); }
        }

        public static readonly DependencyProperty IsSelectedProperty =
        DependencyProperty.Register("IsSelected", typeof(bool), typeof(NodeControl), new PropertyMetadata(false));

        #endregion

        //#region | DragThumbTemplate DP |

        //public ControlTemplate DragThumbTemplate
        //{
        //    get { return (ControlTemplate)GetValue(DragThumbTemplateProperty); }
        //    set { SetValue(DragThumbTemplateProperty, value); }
        //}

        //public static readonly DependencyProperty DragThumbTemplateProperty =
        //DependencyProperty.Register("DragThumbTemplate", typeof(ControlTemplate), typeof(NodeControl));

        //#endregion

        #region | PointDecoratorTemplate DP |

        public ControlTemplate PointDecoratorTemplate
        {
            get { return (ControlTemplate)GetValue(PointDecoratorTemplateProperty); }
            set { SetValue(PointDecoratorTemplateProperty, value); }
        }

        public static readonly DependencyProperty PointDecoratorTemplateProperty =
        DependencyProperty.Register("PointDecoratorTemplate", typeof(ControlTemplate), typeof(NodeControl));

        #endregion

        #region | ResizeDecoratorTemplate DP |

        public ControlTemplate ResizeDecoratorTemplate
        {
            get { return (ControlTemplate)GetValue(ResizeDecoratorTemplateProperty); }
            set { SetValue(ResizeDecoratorTemplateProperty, value); }
        }

        public static readonly DependencyProperty ResizeDecoratorTemplateProperty =
        DependencyProperty.Register("ResizeDecoratorTemplate", typeof(ControlTemplate), typeof(NodeControl));

        #endregion

        #region | IsDragLineOver DP |

        public bool IsDragLineOver
        {
            get { return (bool)GetValue(IsDragLineOverProperty); }
            set { SetValue(IsDragLineOverProperty, value); }
        }

        public static readonly DependencyProperty IsDragLineOverProperty =
        DependencyProperty.Register("IsDragLineOver", typeof(bool), typeof(NodeControl), new PropertyMetadata(false));

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

        #region | Lines |

        private ObservableCollection<ConnectingLineControl> _Lines = new ObservableCollection<ConnectingLineControl>();
        public ObservableCollection<ConnectingLineControl> Lines
        {
            get { return _Lines; }
            set
            {
                ApplyNewValue(ref _Lines, value);
            }
        }

        #endregion

        #region | Position DP |

        public Point Position
        {
            get { return (Point)GetValue(PositionProperty); }
            set { SetValue(PositionProperty, value); }
        }

        public static readonly DependencyProperty PositionProperty =
        DependencyProperty.Register
        (
            "Position",
            typeof(Point),
            typeof(NodeControl),
            new PropertyMetadata(new Point(0, 0), new PropertyChangedCallback(PositionPropertyChanged))
        );

        static void PositionPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as NodeControl;

            sd.NotifyPropertyChanged("Position");
        }

        #endregion

        #endregion

        #region | Ctor |

        static NodeControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeControl), new FrameworkPropertyMetadata(typeof(NodeControl)));
        }

        public NodeControl()
        {
            this.Loaded += NodeControl_Loaded;
        }

        #endregion

        #region | Overrides |

        void NodeControl_Loaded(object sender, RoutedEventArgs e)
        {
        }

        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseDown(e);

            if (Container != null)
            {
                if (!IsSelected)
                {
                    this.IsSelected = true;
                    Container.CurrentSelection = this;
                }
                Focus();
                //e.Handled = true;
            }
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region | Methods |



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
    }
}
