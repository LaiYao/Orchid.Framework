using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Orchid.UI.WPF.StyleGuide
{
    public class ThemeHelper : DependencyObject
    {
        #region | ActiveForeground AP |

        public static Brush GetActiveForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ActiveForegroundProperty);
        }

        public static void SetActiveForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ActiveForegroundProperty, value);
        }

        public static readonly DependencyProperty ActiveForegroundProperty =
        DependencyProperty.RegisterAttached("ActiveForeground", typeof(Brush), typeof(Control));

        #endregion

        #region | FocusedForeground AP |

        public static Brush GetFocusedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(FocusedForegroundProperty);
        }

        public static void SetFocusedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(FocusedForegroundProperty, value);
        }

        public static readonly DependencyProperty FocusedForegroundProperty =
        DependencyProperty.RegisterAttached("FocusedForeground", typeof(Brush), typeof(Control));

        #endregion

        #region | ChechedForeground AP |

        public static Brush GetChechedForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(ChechedForegroundProperty);
        }

        public static void SetChechedForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(ChechedForegroundProperty, value);
        }

        public static readonly DependencyProperty ChechedForegroundProperty =
        DependencyProperty.RegisterAttached("ChechedForeground", typeof(Brush), typeof(Control));

        #endregion

        #region | DisableForeground AP |

        public static Brush GetDisableForeground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DisableForegroundProperty);
        }

        public static void SetDisableForeground(DependencyObject obj, Brush value)
        {
            obj.SetValue(DisableForegroundProperty, value);
        }

        public static readonly DependencyProperty DisableForegroundProperty =
        DependencyProperty.RegisterAttached("DisableForeground", typeof(Brush), typeof(Control),new PropertyMetadata(new SolidColorBrush(Colors.DarkGray)));

        #endregion

        #region | DisableBorderBrush AP |

        public static Brush GetDisableBorderBrush(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DisableBorderBrushProperty);
        }

        public static void SetDisableBorderBrush(DependencyObject obj, Brush value)
        {
            obj.SetValue(DisableBorderBrushProperty, value);
        }

        public static readonly DependencyProperty DisableBorderBrushProperty =
        DependencyProperty.RegisterAttached("DisableBorderBrush", typeof(Brush), typeof(Control));

        #endregion

        #region | DisableBackground AP |

        public static Brush GetDisableBackground(DependencyObject obj)
        {
            return (Brush)obj.GetValue(DisableBackgroundProperty);
        }

        public static void SetDisableBackground(DependencyObject obj, Brush value)
        {
            obj.SetValue(DisableBackgroundProperty, value);
        }

        public static readonly DependencyProperty DisableBackgroundProperty =
        DependencyProperty.RegisterAttached("DisableBackground", typeof(Brush), typeof(Control));

        #endregion
    }
}
