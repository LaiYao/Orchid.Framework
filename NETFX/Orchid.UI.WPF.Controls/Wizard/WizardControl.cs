using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics.Contracts;
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

namespace Orchid.UI.WPF.Controls.Wizard
{
    [TemplatePart(Name = StepsPanelTemplatePartName, Type = typeof(Panel))]
    [TemplatePart(Name = BackButtonTemplatePartName, Type = typeof(Button))]
    [TemplatePart(Name = NextButtonTemplatePartName, Type = typeof(Button))]
    [TemplatePart(Name = FinishButtonTemplatePartName, Type = typeof(Button))]
    [TemplatePart(Name = CancelButtonTemplatePartName, Type = typeof(Button))]
    [ContentProperty("Steps")]
    public class WizardControl : Control
    {
        #region | Constants |

        private const string StepsPanelTemplatePartName = "StepsPanel";
        private const string BackButtonTemplatePartName = "BackButton";
        private const string NextButtonTemplatePartName = "NextButton";
        private const string FinishButtonTemplatePartName = "FinishButton";
        private const string CancelButtonTemplatePartName = "CancelButton";

        #endregion

        #region | Fields |



        #endregion

        #region | Events |

        public event EventHandler<EventArgs> Finished;
        public event EventHandler<EventArgs> Canceled;
        public event EventHandler<WizardNavigationEventArgs> Navigated;
        public event EventHandler<WizardStepChangingEventArgs> Navigating;

        #endregion

        #region | Properties |

        #region | Title DP |

        public object Title
        {
            get { return (object)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
        DependencyProperty.Register("Title", typeof(object), typeof(WizardControl));

        #endregion

        #region | Steps DP |

        public ObservableCollection<WizardStep> Steps
        {
            get { return (ObservableCollection<WizardStep>)GetValue(StepsProperty); }
            set { SetValue(StepsProperty, value); }
        }

        public static readonly DependencyProperty StepsProperty =
        DependencyProperty.Register
        (
            "Steps",
            typeof(ObservableCollection<WizardStep>),
            typeof(WizardControl),
            new PropertyMetadata(null)
        );

        #endregion

        #region | ActiveStep DP |

        public WizardStep ActiveStep
        {
            get { return (WizardStep)GetValue(ActiveStepProperty); }
            set { SetValue(ActiveStepProperty, value); }
        }

        public static readonly DependencyProperty ActiveStepProperty =
        DependencyProperty.Register
        (
            "ActiveStep",
            typeof(WizardStep),
            typeof(WizardControl),
            new PropertyMetadata(new PropertyChangedCallback(ActiveStepPropertyChanged))
        );

        static void ActiveStepPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region | Navigator DP |

        public IWizardNavigator Navigator
        {
            get { return (IWizardNavigator)GetValue(NavigatorProperty); }
            set { SetValue(NavigatorProperty, value); }
        }

        public static readonly DependencyProperty NavigatorProperty =
        DependencyProperty.Register
        (
            "Navigator",
            typeof(IWizardNavigator),
            typeof(WizardControl),
            new PropertyMetadata(new PlainWizardNavigator(), new PropertyChangedCallback(NavigatorPropertyChanged))
        );

        static void NavigatorPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var wizard = (WizardControl)d;
            var oldNavigator = e.OldValue as IWizardNavigator;
            var currentNavigator = e.NewValue as FrameworkElement;

            if (currentNavigator != oldNavigator
                && currentNavigator != null
                && currentNavigator.DataContext == null)
            {
                currentNavigator.DataContext = wizard.DataContext;
            }
        }

        #endregion

        public WizardStep FirstStep { get; private set; }

        public Panel StepsPanel { get; private set; }

        public Button BackButton { get; private set; }

        public Button NextButton { get; private set; }

        public Button FinishButton { get; private set; }

        public Button CancelButton { get; private set; }

        #endregion

        #region | Ctor |

        static WizardControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(WizardControl), new FrameworkPropertyMetadata(typeof(WizardControl)));
        }

        public WizardControl()
        {
            Steps = new ObservableCollection<WizardStep>();
            Steps.CollectionChanged += Steps_CollectionChanged;
        }

        void Steps_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            if (StepsPanel != null)
                StepsPanel.Children.Clear();
            if (BackButton != null)
                BackButton.Click -= OnBackButtonClick;
            if (NextButton != null)
                NextButton.Click -= OnNextButtonClick;
            if (FinishButton != null)
                FinishButton.Click -= OnFinishButtonClick;
            if (CancelButton != null)
                CancelButton.Click -= OnCancelButtonClick;

            StepsPanel = (Panel)GetTemplateChild(StepsPanelTemplatePartName);
            BackButton = (Button)GetTemplateChild(BackButtonTemplatePartName);
            NextButton = (Button)GetTemplateChild(NextButtonTemplatePartName);
            FinishButton = (Button)GetTemplateChild(FinishButtonTemplatePartName);
            CancelButton = (Button)GetTemplateChild(CancelButtonTemplatePartName);

            BackButton.Click += OnBackButtonClick;
            NextButton.Click += OnNextButtonClick;
            FinishButton.Click += OnFinishButtonClick;
            CancelButton.Click += OnCancelButtonClick;

            UpdateNavigationButtonsAccessibility();
            UpdateStepsPanel();
        }

        public bool NavigateBackward()
        {
            if (Steps == null || ActiveStep == null)
                return false;

            var desiredStep = Navigator.GetPreviousStep(Steps, ActiveStep);
            if (desiredStep != null)
            {
                ActivateStep(desiredStep);
                return true;
            }

            return false;
        }

        public bool NavigateForward()
        {
            if (Steps == null || ActiveStep == null)
                return false;

            var desiredStep = Navigator.GetNextStep(Steps, ActiveStep);
            if (desiredStep != null)
            {
                ActivateStep(desiredStep);
                return true;
            }

            return false;
        }

        private void OnBackButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateBackward();
        }

        private void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            NavigateForward();
        }

        private void OnFinishButtonClick(object sender, RoutedEventArgs e)
        {
            RaiseFinished(EventArgs.Empty);
        }

        private void OnCancelButtonClick(object sender, RoutedEventArgs e)
        {
            RaiseCanceled(EventArgs.Empty);
        }

        protected void RaiseFinished(EventArgs e)
        {
            var temp = Finished;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        protected void RaiseCanceled(EventArgs e)
        {
            var temp = Canceled;
            if (temp != null)
            {
                temp(this, e);
            }
        }

        protected virtual void RaiseNavigating(WizardStepChangingEventArgs e)
        {
            var handler = Navigating;
            if (handler != null)
                handler(this, e);
        }

        protected virtual void RaiseNavigated(WizardNavigationEventArgs e)
        {
            var handler = Navigated;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        private void ActivateStep(WizardStep step)
        {
            Contract.Requires<ArgumentException>(Contract.Exists(Steps, s => s == step) || step == null,
                "Step not found in Wizard.Steps collection.");

            var e = new WizardStepChangingEventArgs(step, ActiveStep);
            RaiseNavigating(e);

            if (e.Cancel)
                return;

            ActiveStep = step;
        }

        private void OnStepsCollectionChanged(object d, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                case NotifyCollectionChangedAction.Replace:
                    var steps = e.NewItems.Cast<FrameworkElement>().Where(s => s.DataContext == null);
                    foreach (var step in steps)
                    {
                        step.DataContext = DataContext;
                    }
                    break;
            }
            var oldFirstStep = FirstStep;
            FirstStep = GetFirstStep();

            if (ActiveStep == null)
            {
                if (FirstStep != null)
                    ActivateStep(FirstStep);
            }
            else
            {
                if (Steps.Count == 0)
                    ActivateStep(null);
                else
                {
                    if (oldFirstStep != FirstStep && ActiveStep == oldFirstStep)
                    {
                        ActivateStep(FirstStep);
                    }
                }
            }

            UpdateNavigationButtonsAccessibility();
            UpdateStepsPanel();
        }

        private WizardStep GetFirstStep()
        {
            if (Steps != null)
            {
                if (Steps.Count > 0)
                {
                    return Steps[0];
                }
            }

            return null;
        }

        private void UpdateStepsPanel()
        {
            if (StepsPanel == null)
                return;

            StepsPanel.Children.Clear();
            foreach (var step in Steps)
            {
                StepsPanel.Children.Add(step);
                step.UpdateVisualState();
            }
        }

        private void UpdateNavigationButtonsAccessibility()
        {
            if (BackButton == null || NextButton == null)
            {
                return;
            }

            if (Steps.Count < 1 || ActiveStep == null)
            {
                BackButton.IsEnabled = false;
                NextButton.IsEnabled = false;
            }
            else
            {
                BackButton.IsEnabled = Navigator.IsPreviousStepAvaliable(Steps, ActiveStep);
                NextButton.IsEnabled = Navigator.IsNextStepAvaliable(Steps, ActiveStep);
            }
        }

        //void INotifyDataContextChanged<WizardControl>.OnDataContextChanged(WizardControl sender, DependencyPropertyChangedEventArgs e)
        //{
        //    Contract.Requires(Navigator != null, "Navigator property cannot be null");

        //    foreach (var step in Steps)
        //    {
        //        UpdateDataContext(step, e.OldValue, e.NewValue);
        //    }

        //    var navigator = Navigator as FrameworkElement;
        //    if (navigator != null)
        //    {
        //        UpdateDataContext(navigator, e.OldValue, e.NewValue);
        //    }
        //}

        private static void UpdateDataContext(FrameworkElement element, object oldValue, object newValue)
        {
            Contract.Requires(element != null);

            if (element.DataContext == oldValue)
                element.DataContext = newValue;
        }

        #endregion

    }

    public class WizardNavigationEventArgs : EventArgs
    {
        public WizardNavigationEventArgs(WizardStep newStep, WizardStep oldStep)
        {
            NewStep = newStep;
            OldStep = oldStep;
        }

        public WizardStep NewStep { get; private set; }
        public WizardStep OldStep { get; private set; }
    }

    public class WizardStepChangingEventArgs : WizardNavigationEventArgs
    {
        public WizardStepChangingEventArgs(WizardStep newStep, WizardStep oldStep)
            : base(newStep, oldStep)
        {
        }

        public bool Cancel { get; set; }
    }
}
