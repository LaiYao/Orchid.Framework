using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Orchid.SeedWork.MVVM.Contracts;

namespace Orchid.SeedWork.MVVM
{
    public static class ReundoableManager
    {
        #region Fields

        //public static int TimeOut;

        public static DateTime LastModifiedTime;

        #endregion

        #region Properties

        #region ActionsList

        static Dictionary<string, WeakReference> _ActionsList;
        public static Dictionary<string, WeakReference> ActionsList
        {
            get
            {
                if (_ActionsList == null)
                {
                    _ActionsList = new Dictionary<string, WeakReference>();
                }
                return _ActionsList;
            }
        }

        #endregion

        #region UndoActions

        static Stack<Tuple<string, object>> _UndoActions;
        public static Stack<Tuple<string, object>> UndoActions
        {
            get
            {
                if (_UndoActions == null)
                {
                    _UndoActions = new Stack<Tuple<string, object>>();
                }
                return _UndoActions;
            }
        }

        #endregion

        #region RedoActions

        static Stack<Tuple<string, object>> _RedoActions;
        public static Stack<Tuple<string, object>> RedoActions
        {
            get
            {
                if (_RedoActions == null)
                {
                    _RedoActions = new Stack<Tuple<string, object>>();
                }
                return _RedoActions;
            }
        }

        #endregion

        //public static int MaxLevel { get; set; }

        #region UndoCommand

        static ICommand _UndoCommand;
        public static ICommand UndoCommand
        {
            get
            {
                if (_UndoCommand == null)
                {
                    // TODO: localization
                    _UndoCommand = new DelegateCommand((dr) =>
                    {
                        if (UndoActions.Count == 0) return null;

                        var t = UndoActions.Peek();
                        var cmd = GetAction(t.Item1);
                        var paramForRedo = cmd.Undo(t.Item2);
                        RedoActions.Push(new Tuple<string, object>(cmd.Name, paramForRedo));
                        UndoActions.Pop();
                        return null;
                    },
                    (dr) => UndoActions.Count > 0);
                }
                return _UndoCommand;
            }
        }

        #endregion

        #region RedoCommand

        static ICommand _RedoCommand;
        public static ICommand RedoCommand
        {
            get
            {
                if (_RedoCommand == null)
                {
                    // TODO: localization
                    _RedoCommand = new DelegateCommand((dr) =>
                    {
                        if (RedoActions.Count == 0) return null;
                        var t = RedoActions.Peek();
                        var cmd = GetAction(t.Item1);
                        var paramForUndo = cmd.Redo(t.Item2);
                        UndoActions.Push(new Tuple<string, object>(cmd.Name, paramForUndo));
                        RedoActions.Pop();
                        return null;
                    },
                    (dr) => RedoActions.Count > 0);
                }
                return _RedoCommand;
            }
        }

        #endregion

        #endregion

        public static void ExecuteAction(IReundoable action, object context)
        {
            UndoActions.Push(new Tuple<string, object>(action.Name, context));

            // TODO: handler RedoStack, maybe need to clear RedoStack
            RedoActions.Clear();
        }

        public static void Register(IReundoable action)
        {
            ActionsList.Add(action.Name, new WeakReference(action));
        }

        public static void Unregister(IReundoable action)
        {
            ActionsList.Remove(action.Name);
        }

        public static IReundoable GetAction(string name)
        {
            var reference = ActionsList[name];

            if (!reference.IsAlive)
            {
                ActionsList.Remove(name);
                return null;
            }

            return reference == null ? null : (IReundoable)reference.Target;
        }

    }
}
