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
    public class ResizeThumb : Thumb
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

        static ResizeThumb()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ResizeThumb), new FrameworkPropertyMetadata(typeof(ResizeThumb)));
        }

        public ResizeThumb()
        {
            DragDelta += ResizeThumb_DragDelta;
        }


        #endregion

        #region | Overrides |

        void ResizeThumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (Node != null && Container != null && Node.IsSelected)
            {
                double minLeft = 0, minTop = 0, minDeltaHorizontal = 0, minDeltaVertical = 0;
                double dragDeltaVertical = 0, dragDeltaHorizontal = 0, scale = 0;


                //CalculateDragLimits(selectedNodeControls, out minLeft, out minTop,
                //                    out minDeltaHorizontal, out minDeltaVertical);
                var item = Container.CurrentSelection as NodeControl;
                if (item != null && item.Parent == null)
                {
                    switch (base.VerticalAlignment)
                    {
                        case VerticalAlignment.Bottom:
                            dragDeltaVertical = Math.Min(-e.VerticalChange, minDeltaVertical);
                            scale = (item.ActualHeight - dragDeltaVertical) / item.ActualHeight;
                            DragBottom(scale, item);
                            break;
                        case VerticalAlignment.Top:
                            double top = Canvas.GetTop(item);
                            dragDeltaVertical = Math.Min(Math.Max(-minTop, e.VerticalChange), minDeltaVertical);
                            scale = (item.ActualHeight - dragDeltaVertical) / item.ActualHeight;
                            DragTop(scale, item);
                            break;
                        default:
                            break;
                    }

                    switch (base.HorizontalAlignment)
                    {
                        case HorizontalAlignment.Left:
                            double left = Canvas.GetLeft(item);
                            dragDeltaHorizontal = Math.Min(Math.Max(-minLeft, e.HorizontalChange), minDeltaHorizontal);
                            scale = (item.ActualWidth - dragDeltaHorizontal) / item.ActualWidth;
                            DragLeft(scale, item);
                            break;
                        case HorizontalAlignment.Right:
                            dragDeltaHorizontal = Math.Min(-e.HorizontalChange, minDeltaHorizontal);
                            scale = (item.ActualWidth - dragDeltaHorizontal) / item.ActualWidth;
                            DragRight(scale, item);
                            break;
                        default:
                            break;
                    }
                }

                e.Handled = true;
            }
        }

        #endregion

        #region | Methods |

        private void DragLeft(double scale, NodeControl item)
        {
            //IEnumerable<NodeControl> groupItems = selectionService.GetGroupItems(item).Cast<NodeControl>();
            //double groupLeft = Canvas.GetLeft(item) + item.Width;
            //foreach (NodeControl groupItem in groupItems)
            //{
            //    double groupItemLeft = Canvas.GetLeft(groupItem);
            //    double delta = (groupLeft - groupItemLeft) * (scale - 1);
            //    Canvas.SetLeft(groupItem, groupItemLeft - delta);
            //    groupItem.Width = groupItem.ActualWidth * scale;
            //}
        }

        private void DragTop(double scale, NodeControl item)
        {
            //IEnumerable<NodeControl> groupItems = selectionService.GetGroupItems(item).Cast<NodeControl>();
            //double groupBottom = Canvas.GetTop(item) + item.Height;
            //foreach (NodeControl groupItem in groupItems)
            //{
            //    double groupItemTop = Canvas.GetTop(groupItem);
            //    double delta = (groupBottom - groupItemTop) * (scale - 1);
            //    Canvas.SetTop(groupItem, groupItemTop - delta);
            //    groupItem.Height = groupItem.ActualHeight * scale;
            //}
        }

        private void DragRight(double scale, NodeControl item)
        {
            //IEnumerable<NodeControl> groupItems = selectionService.GetGroupItems(item).Cast<NodeControl>();

            //double groupLeft = Canvas.GetLeft(item);
            //foreach (NodeControl groupItem in groupItems)
            //{
            //    double groupItemLeft = Canvas.GetLeft(groupItem);
            //    double delta = (groupItemLeft - groupLeft) * (scale - 1);

            //    Canvas.SetLeft(groupItem, groupItemLeft + delta);
            //    groupItem.Width = groupItem.ActualWidth * scale;
            //}
        }

        private void DragBottom(double scale, NodeControl item)
        {
            //IEnumerable<NodeControl> groupItems = selectionService.GetGroupItems(item).Cast<NodeControl>();
            //double groupTop = Canvas.GetTop(item);
            //foreach (NodeControl groupItem in groupItems)
            //{
            //    double groupItemTop = Canvas.GetTop(groupItem);
            //    double delta = (groupItemTop - groupTop) * (scale - 1);

            //    Canvas.SetTop(groupItem, groupItemTop + delta);
            //    groupItem.Height = groupItem.ActualHeight * scale;
            //}
        }

        //private void CalculateDragLimits(IEnumerable<NodeControl> selectedItems, out double minLeft, out double minTop, out double minDeltaHorizontal, out double minDeltaVertical)
        //{
        //    minLeft = double.MaxValue;
        //    minTop = double.MaxValue;
        //    minDeltaHorizontal = double.MaxValue;
        //    minDeltaVertical = double.MaxValue;

        //    // drag limits are set by these parameters: canvas top, canvas left, minHeight, minWidth
        //    // calculate min value for each parameter for each item
        //    foreach (NodeControl item in selectedItems)
        //    {
        //        double left = Canvas.GetLeft(item);
        //        double top = Canvas.GetTop(item);

        //        minLeft = double.IsNaN(left) ? 0 : Math.Min(left, minLeft);
        //        minTop = double.IsNaN(top) ? 0 : Math.Min(top, minTop);

        //        minDeltaVertical = Math.Min(minDeltaVertical, item.ActualHeight - item.MinHeight);
        //        minDeltaHorizontal = Math.Min(minDeltaHorizontal, item.ActualWidth - item.MinWidth);
        //    }
        //}

        #endregion
    }
}
