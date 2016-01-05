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

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class NodeDecorator : Control
    {
        #region | Fields |

        Adorner _Adorber;

        #endregion

        #region | Properties |

        #region | IsDecoratorVisiable DP |

        public bool IsDecoratorVisiable
        {
            get { return (bool)GetValue(IsDecoratorVisiableProperty); }
            set { SetValue(IsDecoratorVisiableProperty, value); }
        }

        public static readonly DependencyProperty IsDecoratorVisiableProperty =
        DependencyProperty.Register
        (
            "IsDecoratorVisiable",
            typeof(bool),
            typeof(NodeDecorator),
            new PropertyMetadata(false, new PropertyChangedCallback(IsDecoratorVisiablePropertyChanged))
        );

        static void IsDecoratorVisiablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as NodeDecorator;
            var newValue = (bool)e.NewValue;
            if (newValue)
            {
                sd.ShowDecorator();
            }
            else
            {
                sd.HideDecorator();
            }
        }

        void ShowDecorator()
        { 

        }

        void HideDecorator()
        {
 
        }

        #endregion

        #endregion

        #region | Ctor |

        static NodeDecorator()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeDecorator), new FrameworkPropertyMetadata(typeof(NodeDecorator)));
        }
        
        #endregion

        #region | Overrides |


        
        #endregion
    }
}
