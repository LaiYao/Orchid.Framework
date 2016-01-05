//using System;
//using System.Windows;

//namespace Orchid.UI.WPF.Controls.Wizard
//{
//    public class WizardRoute : FrameworkElement
//    {
//        #region | Events |

//        public event Action AvailabilityChanged;

//        #endregion

//        #region | Properties |

//        #region | IsAvaliable DP |

//        public bool IsAvaliable
//        {
//            get { return (bool)GetValue(IsAvaliableProperty); }
//            set { SetValue(IsAvaliableProperty, value); }
//        }

//        public static readonly DependencyProperty IsAvaliableProperty =
//        DependencyProperty.Register
//        (
//            "IsAvaliable",
//            typeof(bool),
//            typeof(WizardRoute),
//            new PropertyMetadata(true, new PropertyChangedCallback(IsAvaliablePropertyChanged))
//        );

//        static void IsAvaliablePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//        {
//            if (e.OldValue != e.NewValue)
//            {
//                var navigationRoute = (WizardRoute)d;
//                navigationRoute.RaiseAvailabilityChanged();
//            }
//        }

//        void RaiseAvailabilityChanged()
//        {
//            var temp = AvailabilityChanged;
//            if (temp != null)
//                temp();
//        }

//        #endregion

//        public string StepName { get; set; }

//        public string TargetStepName { get; set; }

//        #endregion
//    }
//}
