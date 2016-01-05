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
    public class NodeAdorner : Adorner
    {
        #region | Fields |

        VisualCollection _visuals;

        #endregion

        #region | Ctor |

        static NodeAdorner()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(NodeAdorner), new FrameworkPropertyMetadata(typeof(NodeAdorner)));
        }

        public NodeAdorner(NodeControl node):base(node)
        {
            _visuals = new VisualCollection(this);
        }
        
        #endregion

        #region | Overrides |

        protected override int VisualChildrenCount
        {
            get
            {
                return _visuals.Count;
            }
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

        protected override Visual GetVisualChild(int index)
        {
            return _visuals[index];
        }
        
        #endregion
    }
}
