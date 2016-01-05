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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Orchid.Tool.UI.WPF;

namespace Orchid.UI.WPF.StyleGuide
{
    [TemplatePart(Name = "Part_MinimumButton", Type = typeof(Button))]
    [TemplatePart(Name = "Part_RestoreButton", Type = typeof(Button))]
    [TemplatePart(Name = "Part_CloseButton", Type = typeof(Button))]
    [TemplatePart(Name = "Part_ItemsHost", Type = typeof(StackPanel))]
    public class CaptionButtons : Control
    {
        #region Fields

        Window _parentWindow;
        StackPanel _itemsHost;
        Button _minimumButton;
        Button _restoreButton;
        Button _closeButton;

        #endregion

        #region DPs

        #region MarginOfButton

        public Thickness MarginOfButton
        {
            get { return (Thickness)GetValue(MarginOfButtonProperty); }
            set { SetValue(MarginOfButtonProperty, value); }
        }

        public static readonly DependencyProperty MarginOfButtonProperty =
        DependencyProperty.Register("MarginOfButton", typeof(Thickness), typeof(CaptionButtons));

        #endregion

        #region IsCloseButtonVisible

        public static bool GetIsCloseButtonVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCloseButtonVisibleProperty);
        }

        public static void SetIsCloseButtonVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCloseButtonVisibleProperty, value);
        }

        public static readonly DependencyProperty IsCloseButtonVisibleProperty =
        DependencyProperty.RegisterAttached("IsCloseButtonVisible", typeof(bool), typeof(Window), new UIPropertyMetadata(true));

        #endregion

        #region IsMinumumButtonVisible

        public static bool GetIsMinumumButtonVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsMinumumButtonVisibleProperty);
        }

        public static void SetIsMinumumButtonVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsMinumumButtonVisibleProperty, value);
        }

        public static readonly DependencyProperty IsMinumumButtonVisibleProperty =
        DependencyProperty.RegisterAttached("IsMinumumButtonVisible", typeof(bool), typeof(Window), new UIPropertyMetadata(true));

        #endregion

        #region IsRestoreButtonVisible

        public static bool GetIsRestoreButtonVisible(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsRestoreButtonVisibleProperty);
        }

        public static void SetIsRestoreButtonVisible(DependencyObject obj, bool value)
        {
            obj.SetValue(IsRestoreButtonVisibleProperty, value);
        }

        public static readonly DependencyProperty IsRestoreButtonVisibleProperty =
        DependencyProperty.RegisterAttached("IsRestoreButtonVisible", typeof(bool), typeof(Window), new UIPropertyMetadata(true));

        #endregion

        #region Items

        public static CompositeCollection GetItems(DependencyObject obj)
        {
            return (CompositeCollection)obj.GetValue(ItemsProperty);
        }

        public static void SetItems(DependencyObject obj, CompositeCollection value)
        {
            obj.SetValue(ItemsProperty, value);
        }

        public static readonly DependencyProperty ItemsProperty = DependencyProperty.RegisterAttached(
            "Items",
            typeof(CompositeCollection),
            typeof(Window),
            new UIPropertyMetadata(new PropertyChangedCallback(ItemsPropertyChanged)));

        static void ItemsPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var win = sender as Window;
            var items = e.NewValue as CompositeCollection;
            if (!win.IsLoaded)
            {
                win.Loaded += (o, arg) =>
                {
                    var captionButtons = win.Template.FindName("CaptionButtons", win) as CaptionButtons;
                    if (captionButtons != null && items != null)
                    {
                        foreach (var item in items)
                        {
                            if (item is UIElement)
                            {
                                captionButtons._itemsHost.Children.Insert(0, item as UIElement);
                            }
                        }
                    }
                };
            }
        }

        #endregion

        #endregion

        static CaptionButtons()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CaptionButtons), new FrameworkPropertyMetadata(typeof(CaptionButtons)));
        }

        public CaptionButtons()
        {
            this.Loaded += CaptionButtons_Loaded;
        }

        void CaptionButtons_Loaded(object sender, RoutedEventArgs e)
        {
            _parentWindow = Window.GetWindow(this);

            if (_parentWindow == null) throw new ValueUnavailableException("Cann't find parent window");
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _itemsHost = Template.FindName("Part_ItemsHost", this) as StackPanel;
            _minimumButton = Template.FindName("Part_MinimumButton", this) as Button;
            _closeButton = Template.FindName("Part_CloseButton", this) as Button;
            _restoreButton = Template.FindName("Part_RestoreButton", this) as Button;

            if (_minimumButton != null)
                _minimumButton.Click += (o, e) => { _parentWindow.WindowState = WindowState.Minimized; };

            if (_restoreButton != null)
                _restoreButton.Click += (o, e) =>
                {
                    _parentWindow.WindowState = _parentWindow.WindowState == WindowState.Maximized ?
                        WindowState.Normal : WindowState.Maximized;
                };

            if (_closeButton != null)
                _closeButton.Click += (o, e) => { _parentWindow.Close(); };
        }
    }
}
