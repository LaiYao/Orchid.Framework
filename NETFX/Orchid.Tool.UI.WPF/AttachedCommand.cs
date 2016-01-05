using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Orchid.Tool.UI.WPF
{
    public class AttachedCommand : DependencyObject
    {
        #region | Properties |

        #region | Command AP |

        public static ICommand GetCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(CommandProperty);
        }

        public static void SetCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(CommandProperty, value);
        }


        public static readonly DependencyProperty CommandProperty =
        DependencyProperty.RegisterAttached
        (
            "Command",
            typeof(ICommand),
            typeof(AttachedCommand),
            new PropertyMetadata(null, new PropertyChangedCallback(CommandPropertyChanged))
        );

        static void CommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {

        }

        #endregion

        #region | RoutedEvent AP |

        public static string GetRoutedEvent(DependencyObject obj)
        {
            return (string)obj.GetValue(RoutedEventProperty);
        }

        public static void SetRoutedEvent(DependencyObject obj, string value)
        {
            obj.SetValue(RoutedEventProperty, value);
        }


        public static readonly DependencyProperty RoutedEventProperty =
        DependencyProperty.RegisterAttached
        (
            "RoutedEvent",
            typeof(string),
            typeof(AttachedCommand),
            new PropertyMetadata(string.Empty, new PropertyChangedCallback(RoutedEventPropertyChanged))
        );

        static void RoutedEventPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            String routedEvent = (String)e.NewValue;

            if (!String.IsNullOrEmpty(routedEvent))
            {
                EventHooker eventHooker = new EventHooker();
                eventHooker.AttachedCommandObject = d;
                EventInfo eventInfo = GetEventInfo(d.GetType(), routedEvent);

                if (eventInfo != null)
                {
                    eventInfo.AddEventHandler(d, eventHooker.GetEventHandler(eventInfo));
                }
            }
        }

        #endregion

        #endregion

        private static EventInfo GetEventInfo(Type type, string eventName)
        {
            EventInfo eventInfo = null;
            eventInfo = type.GetTypeInfo().GetDeclaredEvent(eventName);
            if (eventInfo == null)
            {
                Type baseType = type.GetTypeInfo().BaseType;
                if (baseType != null)
                    return GetEventInfo(type.GetTypeInfo().BaseType, eventName);
                else
                    return eventInfo;
            }
            return eventInfo;
        }

        internal sealed class EventHooker
        {
            public DependencyObject AttachedCommandObject { get; set; }

            public Delegate GetEventHandler(EventInfo eventInfo)
            {
                Delegate del = null;
                if (eventInfo == null)
                    throw new ArgumentNullException("eventInfo");

                if (eventInfo.EventHandlerType == null)
                    throw new ArgumentNullException("eventInfo.EventHandlerType");

                if (del == null)
                    del = this.GetType().GetTypeInfo().GetDeclaredMethod("OnEventRaised").CreateDelegate(eventInfo.EventHandlerType, this);

                return del;
            }

            private void OnEventRaised(object sender, object e)
            {
                ICommand command = (ICommand)(sender as DependencyObject).GetValue(AttachedCommand.CommandProperty);

                if (command != null && command.CanExecute(null))
                    command.Execute(null);
            }
        }
    }
}