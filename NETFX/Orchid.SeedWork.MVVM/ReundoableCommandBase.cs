using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Orchid.SeedWork.MVVM.Contracts;
using System.Runtime.Serialization;

namespace Orchid.SeedWork.MVVM
{
    public class ReundoableCommandBase : DelegateCommand, IReundoable
    {
        #region Fields

        Func<object, object> _undo;
        Func<object, object> _redo;

        #endregion

        public ReundoableCommandBase
            (
            Func<object, object> execute,
            Predicate<object> canExecute = null,
            Func<object, object> undo = null,
            Func<object, object> redo = null,
            string name = "",
            string description = ""
            )
            : base(execute, canExecute)
        {
            _undo = undo;
            _redo = redo;
            Name = name;
            Description = description;
            ReundoableManager.Register(this);
        }

        #region Members of IReundoable

        public string Name { get; set; }

        public string Description { get; set; }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var context = _execute(parameter);
                if (_redo != null && _undo != null)
                {
                    ReundoableManager.ExecuteAction(this, context);
                }
            }
        }

        public object Undo(object param)
        {
            var result = _undo(param);
            if (Undoing != null)
            {
                Undoing(this, new EventArgs());
            }
            return result;
        }

        public object Redo(object param)
        {
            if (Redoing != null)
            {
                Redoing(this, new EventArgs());
            }
            return _redo(param);
        }

        [field: IgnoreDataMember]
        public event EventHandler Undoing;

        [field: IgnoreDataMember]
        public event EventHandler Redoing;

        #endregion
    }
}
