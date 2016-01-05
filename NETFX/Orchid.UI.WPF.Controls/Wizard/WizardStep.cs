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

namespace Orchid.UI.WPF.Controls.Wizard
{
    [TemplatePart(Name = TitleContentControlTemplatePartName, Type = typeof(ContentControl))]
    [TemplateVisualState(Name = SelectedVisualStateName, GroupName = SelectionsStatesGroupName)]
    [TemplateVisualState(Name = UnselectedVisualStateName, GroupName = SelectionsStatesGroupName)]
    public class WizardStep : Control
    {
        #region | Constants |

        private const string TitleContentControlTemplatePartName = "TitleContentControl";
        private const string SelectionsStatesGroupName = "SelectionStates";
        private const string SelectedVisualStateName = "Selected";
        private const string UnselectedVisualStateName = "Unselected";

        #endregion

        #region | Fields |

        ContentControl _TitleContentControl;

        #endregion

        #region | Properties |

        #region | Title DP |

        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register
        (
            "Title",
            typeof(object),
            typeof(WizardStep),
            new PropertyMetadata(new PropertyChangedCallback(TitlePropertyChanged))
        );

        static void TitlePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as WizardStep;
            sd.UpdateTitleVisuals();
        }

        #endregion

        #region | TitleTemplate DP |

        public DataTemplate TitleTemplate
        {
            get { return (DataTemplate)GetValue(TitleTemplateProperty); }
            set { SetValue(TitleTemplateProperty, value); }
        }

        public static readonly DependencyProperty TitleTemplateProperty =
        DependencyProperty.Register
        (
            "TitleTemplate",
            typeof(DataTemplate),
            typeof(WizardStep),
            new PropertyMetadata(new PropertyChangedCallback(TitleTemplatePropertyChanged))
        );

        static void TitleTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var sd = d as WizardStep;
            sd.UpdateTitleVisuals();
        }

        #endregion

        #region | Description DP |

        public object Description
        {
            get { return (object)GetValue(DescriptionProperty); }
            set { SetValue(DescriptionProperty, value); }
        }

        public static readonly DependencyProperty DescriptionProperty =
        DependencyProperty.Register("Description", typeof(object), typeof(WizardStep));

        #endregion

        #region | ParentWizardControl |

        private WizardControl _ParentWizardControl;
        public WizardControl ParentWizardControl
        {
            get
            {
                if (_ParentWizardControl == null)
                {
                    _ParentWizardControl = Parent as WizardControl;
                    if (_ParentWizardControl == null)
                    {
                        _ParentWizardControl = this.FindVisualAncestorByType<WizardControl>();
                    }
                }

                return _ParentWizardControl;
            }
        }

        #endregion

        #endregion

        #region | Ctor |

        static WizardStep()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WizardStep), new FrameworkPropertyMetadata(typeof(WizardStep)));
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _TitleContentControl = (ContentControl)GetTemplateChild(TitleContentControlTemplatePartName);

            UpdateTitleVisuals();
            ChangeVisualState(false);
        }

        void UpdateTitleVisuals()
        {
            if (_TitleContentControl == null)
                return;

            _TitleContentControl.Content = Title;
            _TitleContentControl.ContentTemplate = TitleTemplate;
        }

        void ChangeVisualState(bool useTransitions)
        {
            if (ParentWizardControl != null
                && ReferenceEquals(ParentWizardControl.ActiveStep, this))
            {
                VisualStateManager.GoToState(this, SelectedVisualStateName, useTransitions);
            }
            else
            {
                VisualStateManager.GoToState(this, UnselectedVisualStateName, useTransitions);
            }
        }

        internal void UpdateVisualState()
        {
            ChangeVisualState(true);
        }
    }
}
