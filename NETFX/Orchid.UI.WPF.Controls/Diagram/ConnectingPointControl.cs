using Orchid.SeedWork.Core;
using Orchid.SeedWork.UI.Contract;
using Orchid.Tool.UI.WPF;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace Orchid.UI.WPF.Controls.Diagram
{
    public class ConnectingPointControl : Control
    {
        #region | Fields |

        Point? _dragStartPosition;

        DesignerCanvas _canvas;

        #endregion

        #region | Properties |

        #region | Parent |

        private NodeControl _ParentNode;
        public NodeControl ParentNode
        {
            get { return _ParentNode; }
            set
            {
                if (value == _ParentNode)
                    return;
                _ParentNode = value;
            }
        }

        #endregion

        #endregion

        #region | Ctor |

        static ConnectingPointControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ConnectingPointControl), new FrameworkPropertyMetadata(typeof(ConnectingPointControl)));
        }

        public ConnectingPointControl()
        {
            this.Loaded += ConnectingPointControl_Loaded;
            this.Unloaded += ConnectingPointControl_Unloaded;
        }

        void ConnectingPointControl_Loaded(object sender, RoutedEventArgs e)
        {
            ParentNode = this.FindVisualAncestorByType<NodeControl>();
        }

        void ConnectingPointControl_Unloaded(object sender, RoutedEventArgs e)
        {
            _canvas = null;
        }

        #endregion

        #region | Overrides |

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);

            if (_canvas == null)
            {
                _canvas = this.FindVisualAncestorByType<DesignerCanvas>();
            }

            if (_canvas != null)
            {
                _dragStartPosition = new Point?(e.GetPosition(_canvas));
            }

            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.LeftButton != MouseButtonState.Pressed) _dragStartPosition = null;

            if (_dragStartPosition.HasValue)
            {
                if (_canvas != null)
                {
                    var adornerLayer = AdornerLayer.GetAdornerLayer(_canvas);
                    if (adornerLayer != null)
                    {
                        var adorner = new ConnectingPointAdorner(_canvas, this);

                        adornerLayer.Add(adorner);
                        e.Handled = true;
                    }
                }
                else
                {
                    Trace.TraceWarning("a connection point expect its ancestor is designer canvas!");
                }
            }
        }

        #endregion

        #region | Methods |



        #endregion
    }
}
