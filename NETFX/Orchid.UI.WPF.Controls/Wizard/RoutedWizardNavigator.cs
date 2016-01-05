//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using System.Windows;
//using System.Windows.Controls;
//using System.Windows.Data;
//using System.Windows.Documents;
//using System.Windows.Input;
//using System.Windows.Markup;
//using System.Windows.Media;
//using System.Windows.Media.Imaging;
//using System.Windows.Navigation;
//using System.Windows.Shapes;

//namespace Orchid.UI.WPF.Controls.Wizard
//{
//    [ContentProperty("Routes")]
//    public class RoutedWizardNavigator : FrameworkElement, IWizardNavigator, INotifyDataContextChanged<RoutedWizardNavigator>
//    {
//        #region | Properties |

//        #region | Routes DP |

//        public ObservableCollection<WizardRoute> Routes
//        {
//            get { return (ObservableCollection<WizardRoute>)GetValue(RoutesProperty); }
//            set { SetValue(RoutesProperty, value); }
//        }

//        public static readonly DependencyProperty RoutesProperty =
//        DependencyProperty.Register("Routes", typeof(ObservableCollection<WizardRoute>), typeof(RoutedWizardNavigator));

//        #endregion

//        #endregion

//        #region | Ctor |

//        public RoutedWizardNavigator()
//        {
//            Routes = new ObservableCollection<WizardRoute>();

//            Routes.CollectionChanged += OnRoutesCollectionChanged;

//            NotifyDataContextChanged<RoutedWizardNavigator>.BindContext(this);
//        }

//        #endregion

//        #region Methods

//        private static WizardStep GetStepByName(IEnumerable<WizardStep> steps, string stepName)
//        {
//            Contract.Requires(!string.IsNullOrWhiteSpace(stepName), "Illegal 'stepName' argument value.");

//            return steps.Single(s => s.Name == stepName);
//        }

//        private string GetNextStepName(string stepName)
//        {
//            var route = Routes.Single(r => r.StepName == stepName && r.IsAvaliable);
//            var nextStepName = route.TargetStepName;

//            return nextStepName;
//        }

//        private string GetPreviousStepName(IList<WizardStep> steps, string stepName)
//        {
//            var passedSteps = new Stack<string>();
//            var currentStepName = steps[0].Name;

//            while (currentStepName != stepName)
//            {
//                if (passedSteps.Contains(currentStepName))
//                    throw new Exception("Recursion detected. Navigation rules contain recurring step.");
//                passedSteps.Push(currentStepName);
//                currentStepName = GetNextStepName(currentStepName);
//            }

//            return passedSteps.Peek();
//        }

//        private bool IsNextStepAvaliable(string stepName)
//        {
//            return Routes.Any(r => r.StepName == stepName);
//        }

//        private bool IsPreviousStepAvaliable(string stepName)
//        {
//            return Routes.Any(r => r.TargetStepName == stepName);
//        }

//        public bool IsNextStepAccessible(string stepName)
//        {
//            if (!IsNextStepAvaliable(stepName))
//                return false;

//            var openRoutes = Routes.Where(r => r.StepName == stepName && r.IsAvaliable);

//            if (openRoutes.Count() == 1)
//                return true;
//            if (openRoutes.Count() > 1)
//                throw new Exception("Ambigous routes detected. More than one navigation route is avaliable for navigation.");

//            return false;
//        }

//        public WizardStep GetNextStep(IList<WizardStep> steps, WizardStep step)
//        {
//            if (!IsNextStepAccessible(steps, step))
//                return null;

//            return GetStepByName(steps, GetNextStepName(step.Name));
//        }

//        public WizardStep GetPreviousStep(IList<WizardStep> steps, WizardStep step)
//        {
//            if (!IsPreviousStepAvaliable(steps, step))
//                return null;

//            return GetStepByName(steps, GetPreviousStepName(steps, step.Name));
//        }

//        public bool IsNextStepAvaliable(IList<WizardStep> steps, WizardStep step)
//        {
//            return IsNextStepAvaliable(step.Name);
//        }

//        public bool IsPreviousStepAvaliable(IList<WizardStep> steps, WizardStep step)
//        {
//            return IsPreviousStepAvaliable(step.Name);
//        }

//        public bool IsNextStepAccessible(IList<WizardStep> steps, WizardStep step)
//        {
//            return IsNextStepAccessible(step.Name);
//        }

//        private void OnRoutesCollectionChanged(object d, NotifyCollectionChangedEventArgs e)
//        {
//            switch (e.Action)
//            {
//                case NotifyCollectionChangedAction.Add:
//                case NotifyCollectionChangedAction.Replace:
//                    var rules = e.NewItems.Cast<FrameworkElement>().Where(r => r.DataContext == null);
//                    foreach (var rule in rules)
//                    {
//                        rule.DataContext = DataContext;
//                    }
//                    break;
//            }
//        }

//        void INotifyDataContextChanged<RoutedWizardNavigator>.OnDataContextChanged(RoutedWizardNavigator sender, DependencyPropertyChangedEventArgs e)
//        {
//            foreach (var route in Routes)
//            {
//                if (route.DataContext == e.OldValue)
//                    route.DataContext = e.NewValue;
//            }
//        }

//        #endregion
//    }
//}
