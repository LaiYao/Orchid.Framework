using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Input;

namespace Orchid.SeedWork.MVVM
{
    [DataContract]
    public class DelegateCommand : ICommand
    {
        #region | Fields |

        protected Func<object, object> _execute;
        protected Predicate<object> _canExecute;

        #endregion

        #region | Ctor |

        public DelegateCommand(Func<object, object> execute, Predicate<object> canExecute = null)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");
            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        public void NotifyCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #region | Members of ICommand |

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public virtual void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion
    }
}
