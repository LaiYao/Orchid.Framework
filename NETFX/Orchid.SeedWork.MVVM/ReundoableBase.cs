using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using Orchid.SeedWork.MVVM.DataAnnotations;
using Orchid.SeedWork.MVVM.Contracts;
using System.Runtime.Serialization;

namespace Orchid.SeedWork.MVVM
{
    [DataContract]
    public abstract class ReundoableBase : NotifiableBase
    {
        #region Fields

        public static ReundoableCommandBase ChangePropertyCommand = new ReundoableCommandBase(
            (dr) =>
            {
                return dr;
            },
            null,
            SetPropertyValueForReundo,
            SetPropertyValueForReundo,
            "$ChangeProperty$",
            "Change property");

        static object SetPropertyValueForReundo(object previousValue)
        {
            var context = previousValue as Tuple<WeakReference, string, object>;
            if (context == null)
            {
                Debug.Assert(false);
                return previousValue;
            }
            var obj = context.Item1 as WeakReference;
            if (obj == null || obj.Target == null)
            {
                Debug.Assert(false);
                return previousValue;
            }
            // TODO: 
            //if (!obj.IsAlive)
            //{

            //}

            var property = ReundoableProperties[obj.Target.GetType()].First(dr0 => dr0.Name == context.Item2);
            if (property == null)
            {
                Debug.Assert(false);
                return previousValue;
            }
            var reundoObj = obj.Target as ReundoableBase;
            if (reundoObj == null)
            {
                Debug.Assert(false);
                return previousValue;
            }
            var value = property.GetValue(reundoObj, null);
            var ignoreReundo = reundoObj.IgnoreReundo;
            reundoObj.IgnoreReundo = true;
            property.SetValue(reundoObj, context.Item3, null);
            reundoObj.IgnoreReundo = ignoreReundo;

            return new Tuple<WeakReference, string, object>(new WeakReference(reundoObj), context.Item2, value);
        }

        int _batchingInterval;

        #endregion

        protected ReundoableBase()
        {
            _batchingInterval = 300;
            this.PropertyChanging += ReundoableBase_PropertyChanging;
            this.PropertyChanged += ReundoableBase_PropertyChanged;

            var type = this.GetType();
            if (!ReundoableProperties.Keys.Contains(type))
            {
                ReundoableProperties.Add(type, GetReundoableProperties(type));
            }
        }

        Tuple<WeakReference, string, object> propertyPair;
        void ReundoableBase_PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var property = ReundoableProperties[this.GetType()].FirstOrDefault(dr => dr.Name == e.PropertyName);
            if (property != null && !IgnoreReundo)
            {
                propertyPair = new Tuple<WeakReference, string, object>(new WeakReference(this), e.PropertyName, property.GetValue(this, null));
            }
        }

        void ReundoableBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var property = ReundoableProperties[this.GetType()].FirstOrDefault(dr => dr.Name == e.PropertyName);
            if (property != null && propertyPair != null && !IgnoreReundo)
            {
                var currentTime = DateTime.Now;

                // continuous action
                // we need store 2 same actions for continuous action, the last one will be replaced with last action
                if (currentTime.Date == ReundoableManager.LastModifiedTime.Date
                    && (currentTime.TimeOfDay.TotalMilliseconds - ReundoableManager.LastModifiedTime.TimeOfDay.TotalMilliseconds) < _batchingInterval)
                {
                    if (ReundoableManager.UndoActions.Count > 1)
                    {
                        var action1 = ReundoableManager.UndoActions.Pop();
                        var action2 = ReundoableManager.UndoActions.Pop();
                        bool needtoReplace = false;

                        if (action1.Item1 == "$ChangeProperty$" && action2.Item1 == "$ChangeProperty$")
                        {
                            var v1 = action1.Item2 as Tuple<WeakReference, string, object>;
                            var v2 = action2.Item2 as Tuple<WeakReference, string, object>;
                            Debug.Assert(v1 != null && v2 != null
                                && v1.Item1.Target != null && v2.Item1.Target != null
                                && v1.Item1.Target is ReundoableBase && v2.Item1.Target is ReundoableBase);
                            if (v1.Item1.Target.Equals(this) && v2.Item1.Target.Equals(this) && v1.Item2 == v2.Item2)
                            {
                                // is continuous action
                                needtoReplace = true;
                            }
                        }

                        ReundoableManager.UndoActions.Push(action2);
                        if (needtoReplace)
                        {
                            ChangePropertyCommand.Execute(propertyPair);
                        }
                        else
                        {
                            ReundoableManager.UndoActions.Push(action1);
                            ChangePropertyCommand.Execute(propertyPair);
                        }
                    }
                    else
                    {
                        ChangePropertyCommand.Execute(propertyPair);
                    }
                }
                // normal action
                else
                {
                    ChangePropertyCommand.Execute(propertyPair);
                }

                ReundoableManager.LastModifiedTime = currentTime;
                // TODO:serializable?
            }
        }

        #region Properties

        static Dictionary<Type, List<PropertyInfo>> _ReundoableProperties;
        public static Dictionary<Type, List<PropertyInfo>> ReundoableProperties
        {
            get
            {
                if (_ReundoableProperties == null)
                {
                    _ReundoableProperties = new Dictionary<Type, List<PropertyInfo>>();
                }
                return _ReundoableProperties;
            }
        }

        static List<PropertyInfo> GetReundoableProperties(Type type)
        {
            var result = new List<PropertyInfo>();

            var propertiesList = type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
            foreach (var item in propertiesList)
            {
                var attrs = item.GetCustomAttributes(typeof(ReundoableAttribute), true);
                if (attrs != null && attrs.Count() != 0)
                {
                    result.Add(item);
                }
            }

            return result;
        }

        #endregion

        #region Name

        string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                if (value != _Name)
                {
                    NotifyPropertyChanging("Name");
                    _Name = value;
                    NotifyPropertyChanged("Name");
                }
            }
        }

        #endregion

        [DefaultValue(false)]
        [DataMember]
        public bool IgnoreReundo { get; set; }
    }
}
