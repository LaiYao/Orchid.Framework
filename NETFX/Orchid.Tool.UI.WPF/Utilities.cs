using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;

namespace Orchid.Tool.UI.WPF
{
    public static class Utilities
    {
        #region Find Elements

        /// <summary>
        /// Finds the logical ancestor according to the predicate.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static DependencyObject FindLogicalAncestor(this DependencyObject startElement, Predicate<DependencyObject> condition)
        {
            DependencyObject o = startElement;
            while ((o != null) && !condition(o))
            {
                o = LogicalTreeHelper.GetParent(o);
            }
            return o;
        }

        /// <summary>
        /// Finds the logical ancestor by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startElement">The start element.</param>
        /// <returns></returns>
        public static T FindLogicalAncestorByType<T>(this DependencyObject startElement) where T : DependencyObject
        {
            return (T)FindLogicalAncestor(startElement, o => o is T);
        }

        /// <summary>
        /// Finds the logical root.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <returns></returns>
        public static DependencyObject FindLogicalRoot(this DependencyObject startElement)
        {
            DependencyObject o = null;
            while (startElement != null)
            {
                o = startElement;
                startElement = LogicalTreeHelper.GetParent(startElement);
            }
            return o;
        }

        /// <summary>
        /// Finds the visual ancestor according to the predicate.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static DependencyObject FindVisualAncestor(this DependencyObject startElement, Predicate<DependencyObject> condition)
        {
            DependencyObject o = startElement;
            while ((o != null) && !condition(o))
            {
                o = VisualTreeHelper.GetParent(o);
            }
            return o;
        }

        /// <summary>
        /// Finds the visual ancestor by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startElement">The start element.</param>
        /// <returns></returns>
        public static T FindVisualAncestorByType<T>(this DependencyObject startElement) where T : DependencyObject
        {
            return (T)FindVisualAncestor(startElement, o => o is T);
        }

        /// <summary>
        /// Finds the visual descendant.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static DependencyObject FindVisualDescendant(this DependencyObject startElement, Predicate<DependencyObject> condition)
        {
            if (startElement != null)
            {
                if (condition(startElement))
                {
                    return startElement;
                }
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(startElement); ++i)
                {
                    DependencyObject o = FindVisualDescendant(VisualTreeHelper.GetChild(startElement, i), condition);
                    if (o != null)
                    {
                        return o;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the visual descendant by type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="startElement">The start element.</param>
        /// <returns></returns>
        public static T FindVisualDescendantByType<T>(this DependencyObject startElement) where T : DependencyObject
        {
            return (T)FindVisualDescendant(startElement, o => o is T);
        }

        /// <summary>
        /// Finds the visual root.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <returns></returns>
        public static DependencyObject FindVisualRoot(this DependencyObject startElement)
        {
            return FindVisualAncestor(startElement, o => VisualTreeHelper.GetParent(o) == null);
        }

        /// <summary>
        /// Gets the visual children.
        /// </summary>
        /// <param name="parent">The parent.</param>
        /// <returns></returns>
        public static IEnumerable<Visual> GetVisualChildren(this Visual parent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; ++i)
            {
                yield return (Visual)VisualTreeHelper.GetChild(parent, i);
            }
        }

        /// <summary>
        /// Gets specified visual state group.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <param name="groupName">Name of the group.</param>
        /// <returns></returns>
        public static VisualStateGroup TryGetVisualStateGroup(this DependencyObject dependencyObject, string groupName)
        {
            FrameworkElement root = GetImplementationRoot(dependencyObject);
            if (root == null)
            {
                return null;
            }
            return (from @group in VisualStateManager.GetVisualStateGroups(root).OfType<VisualStateGroup>()
                    where string.CompareOrdinal(groupName, @group.Name) == 0
                    select @group).FirstOrDefault<VisualStateGroup>();
        }

        /// <summary>
        /// Gets the implementation root.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns></returns>
        public static FrameworkElement GetImplementationRoot(this DependencyObject dependencyObject)
        {
            if (1 != VisualTreeHelper.GetChildrenCount(dependencyObject))
            {
                return null;
            }
            return (VisualTreeHelper.GetChild(dependencyObject, 0) as FrameworkElement);
        }

        /// <summary>
        /// Returns a collection of the visual ancestor elements of specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>
        /// A collection that contains the ancestors elements.
        /// </returns>
        public static IEnumerable<DependencyObject> Ancestors(this DependencyObject dependencyObject)
        {
            var parent = dependencyObject;
            while (true)
            {
                parent = GetParent(parent);
                if (parent != null)
                {
                    yield return parent;
                }
                else
                {
                    break;
                }
            }
        }

        /// <summary>
        /// Returns a collection of visual elements that contain specified object, and the ancestors of specified object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object.</param>
        /// <returns>
        /// A collection that contains the ancestors elements and the object itself.
        /// </returns>
        public static IEnumerable<DependencyObject> AncestorsAndSelf(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }

            var parent = dependencyObject;
            while (true)
            {
                if (parent != null)
                {
                    yield return parent;
                }
                else
                {
                    break;
                }
                parent = GetParent(parent);
            }
        }

        /// <summary>
        /// Gets the parent for specified dependency object.
        /// </summary>
        /// <param name="dependencyObject">The dependency object</param>
        /// <returns>The parent object or null if there is no parent.</returns>
        public static DependencyObject GetParent(this DependencyObject dependencyObject)
        {
            if (dependencyObject == null)
            {
                throw new ArgumentNullException("dependencyObject");
            }

            var ce = dependencyObject as ContentElement;
            if (ce != null)
            {
                var parent = ContentOperations.GetParent(ce);
                if (parent != null)
                {
                    return parent;
                }

                var fce = ce as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            return VisualTreeHelper.GetParent(dependencyObject);
        }

        #endregion

        public static bool IsMouseInArea(this UIElement source)
        {
            var pt = Mouse.GetPosition(source);
            return IsMouseInArea(source, pt);
        }

        public static bool IsMouseInArea(this UIElement source, Point pos)
        {
            return pos.X > 0 && pos.Y > 0 && pos.X < source.RenderSize.Width && pos.Y < source.RenderSize.Height;
        }
    }
}
