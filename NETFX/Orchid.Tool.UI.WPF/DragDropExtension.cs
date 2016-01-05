using Orchid.SeedWork.UI.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace Orchid.Tool.UI.WPF
{
    public class DragDropEventArgs : EventArgs
    {
        public DataObject Data { get; set; }
    }

    public class DragDropExtention : DependencyObject
    {
        #region | For drag source |

        static Point DragStartPoint = new Point(0, 0);

        public static event EventHandler<DragDropEventArgs> DragEnded;

        #region | IsDragSource AP |

        public static bool GetIsDragSource(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDragSourceProperty);
        }

        public static void SetIsDragSource(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDragSourceProperty, value);
        }

        public static readonly DependencyProperty IsDragSourceProperty =
            DependencyProperty.RegisterAttached("IsDragSource", typeof(bool), typeof(DragDropExtention),
            new PropertyMetadata(false, new PropertyChangedCallback(IsDragSourcePropertyChanged)));

        static void IsDragSourcePropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var dragSource = sender as UIElement;
            if ((bool)e.NewValue)
            {
                dragSource.PreviewMouseLeftButtonDown += dragSource_PreviewMouseLeftButtonDown;
                dragSource.PreviewMouseMove += dragSource_PreviewMouseMove;
            }
            else
            {
                dragSource.PreviewMouseLeftButtonDown -= dragSource_PreviewMouseLeftButtonDown;
                dragSource.PreviewMouseMove -= dragSource_PreviewMouseMove;
            }
        }

        static void dragSource_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragStartPoint = e.GetPosition(null);
        }

        static void dragSource_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var diff = DragStartPoint - e.GetPosition(null);
                if (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance
                    || Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    var dragSource = sender as FrameworkElement;
                    var dragHandler = GetDragHandler(dragSource);
                    if (dragHandler != null)
                    {
                        if (dragSource is ItemsControl)
                        {
                            var dragSourceList = dragSource as ItemsControl;
                            if (dragSourceList.HasItems
                                && dragSourceList.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                            {
                                var itemType = (dragSource as ItemsControl).ItemContainerGenerator.ContainerFromIndex(0).GetType();
                                var actualDragSource = (e.OriginalSource as UIElement).FindVisualAncestor(dr => dr.GetType() == itemType) as FrameworkElement;
                                if (actualDragSource != null && dragHandler.CanDrag(actualDragSource.DataContext))
                                {
                                    var dragDropData = new DataObject("tvi", actualDragSource.DataContext);
                                    DragDrop.DoDragDrop(actualDragSource, dragDropData, DragDropEffects.All);
                                    if (DragEnded != null) DragEnded(sender, new DragDropEventArgs { Data = dragDropData });
                                    return;
                                }
                            }
                        }
                        else
                        {
                            if (dragHandler.CanDrag(dragSource.DataContext))
                            {
                                var dragDropData = new DataObject("tvi", dragSource.DataContext);
                                DragDrop.DoDragDrop(dragSource, dragDropData, DragDropEffects.All);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region | DragHandler AP |

        public static IDragSource GetDragHandler(DependencyObject obj)
        {
            return (IDragSource)obj.GetValue(DragHandlerProperty);
        }

        public static void SetDragHandler(DependencyObject obj, IDragSource value)
        {
            obj.SetValue(DragHandlerProperty, value);
        }

        public static readonly DependencyProperty DragHandlerProperty =
            DependencyProperty.RegisterAttached("DragHandler", typeof(IDragSource), typeof(DragDropExtention));

        #endregion

        #endregion

        #region | For drop target |

        #region | IsDropTarget AP |

        public static bool GetIsDropTarget(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsDropTargetProperty);
        }

        public static void SetIsDropTarget(DependencyObject obj, bool value)
        {
            obj.SetValue(IsDropTargetProperty, value);
        }

        public static readonly DependencyProperty IsDropTargetProperty =
            DependencyProperty.RegisterAttached("IsDropTarget", typeof(bool), typeof(DragDropExtention),
            new PropertyMetadata(false, new PropertyChangedCallback(IsDropTargetPropertyChanged)));

        static void IsDropTargetPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var dropTarget = sender as FrameworkElement;
            if ((bool)e.NewValue)
            {
                dropTarget.PreviewDragEnter += dropTarget_PreviewDragEnter;
                dropTarget.PreviewDragOver += dropTarget_PreviewDragOver;
                dropTarget.PreviewDrop += dropTarget_PreviewDrop;
                dropTarget.AllowDrop = true;
            }
            else
            {
                dropTarget.AllowDrop = false;
                dropTarget.PreviewDragEnter -= dropTarget_PreviewDragEnter;
                dropTarget.PreviewDragOver -= dropTarget_PreviewDragOver;
                dropTarget.PreviewDrop -= dropTarget_PreviewDrop;
            }
        }

        static void dropTarget_PreviewDragEnter(object sender, DragEventArgs e)
        {
            dropTarget_PreviewDragOver(sender, e);
        }

        static void dropTarget_PreviewDragOver(object sender, DragEventArgs e)
        {
            var dropTarget = sender as FrameworkElement;
            var dropHandler = GetDropHandler(dropTarget);
            var dropable = false;
            if (dropTarget != null && dropHandler != null)
            {
                var ddData = e.Data.GetData("tvi") ?? e.Data.GetData(DataFormats.Serializable);

                if (dropTarget is ItemsControl)
                {
                    var dropTargetList = dropTarget as ItemsControl;
                    if (dropTargetList.HasItems
                        && dropTargetList.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                    {
                        var dropTargetType = dropTargetList.ItemContainerGenerator.ContainerFromIndex(0).GetType();
                        var actualDropTarget = (e.OriginalSource as UIElement).FindVisualAncestor(dr => dr.GetType() == dropTargetType) as FrameworkElement;
                        // ex: treeviewItem
                        if (actualDropTarget != null && actualDropTarget is ItemsControl)
                        {
                            if (dropHandler.CanDrop(ddData, actualDropTarget.DataContext))
                            {
                                dropable = true;
                            }
                        }
                        // ex: listBox
                        else
                        {
                            if (dropHandler.CanDrop(ddData, dropTarget.DataContext))
                            {
                                dropable = true;
                            }
                        }
                    }
                }
                else
                {
                    if (dropHandler.CanDrop(ddData, dropTarget.DataContext))
                    {
                        dropable = true;
                    }
                }
            }

            if (dropable)
            {
                e.Effects = DragDropEffects.Move;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        static void dropTarget_PreviewDrop(object sender, DragEventArgs e)
        {
            var dropTarget = sender as FrameworkElement;
            var dropHandler = GetDropHandler(dropTarget);
            if (dropTarget != null && dropHandler != null)
            {
                var ddData = e.Data.GetData("tvi") ?? e.Data.GetData(DataFormats.Serializable);

                if (dropTarget is ItemsControl)
                {
                    var dropTargetList = dropTarget as ItemsControl;
                    if (dropTargetList.HasItems
                        && dropTargetList.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
                    {
                        var dropTargetType = dropTargetList.ItemContainerGenerator.ContainerFromIndex(0).GetType();
                        var actualDropTarget = (e.OriginalSource as UIElement).FindVisualAncestor(dr => dr.GetType() == dropTargetType) as FrameworkElement;
                        // ex: treeviewItem
                        if (actualDropTarget != null && actualDropTarget is ItemsControl)
                        {
                            if (dropHandler.CanDrop(ddData, actualDropTarget.DataContext))
                            {
                                dropHandler.Drop(ddData, actualDropTarget.DataContext, e.GetPosition(actualDropTarget));
                            }
                        }
                        // ex: listBox
                        else
                        {
                            if (dropHandler.CanDrop(ddData, dropTarget.DataContext))
                            {
                                dropHandler.Drop(ddData, dropTarget.DataContext, e.GetPosition(actualDropTarget));
                            }
                        }
                    }
                }
                else
                {
                    if (dropHandler.CanDrop(ddData, dropTarget.DataContext))
                    {
                        dropHandler.Drop(ddData, dropTarget.DataContext, e.GetPosition(dropTarget));
                    }
                }
            }
        }

        #endregion

        #region | DropHandler AP |

        public static IDropTarget GetDropHandler(DependencyObject obj)
        {
            return (IDropTarget)obj.GetValue(DropHandlerProperty);
        }

        public static void SetDropHandler(DependencyObject obj, IDropTarget value)
        {
            obj.SetValue(DropHandlerProperty, value);
        }

        public static readonly DependencyProperty DropHandlerProperty =
            DependencyProperty.RegisterAttached("DropHandler", typeof(IDropTarget), typeof(DragDropExtention));

        #endregion

        #endregion
    }
}
