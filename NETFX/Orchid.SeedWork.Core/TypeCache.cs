// Ref: http://www.codeproject.com/Articles/109868/General-DynamicObject-Proxy-and-Fast-Reflection-Pr
using Orchid.SeedWork.Core.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Orchid.SeedWork.Core
{
    public static class TypeCache<T> where T : class
    {
        #region | Constants |

        const BindingFlags Flags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public;

        #endregion

        #region | Fields |

        static object _locker = new object();
        static Type _type;

        #endregion

        #region | Properties |

        readonly static Dictionary<string, PropertyInfoEx> Properties = new Dictionary<string, PropertyInfoEx>();

        #endregion

        #region | Ctor |

        static TypeCache()
        {
            _type = typeof(T);
        }

        #endregion

        public static PropertyInfoEx GetPropertyInfoEx(string propertyName)
        {
            PropertyInfoEx result = null;

            if (!Properties.TryGetValue(propertyName, out result))
            {
                var propertyInfo = typeof(T).GetProperty(propertyName, Flags | BindingFlags.NonPublic);
                if (propertyInfo != null)
                {
                    Properties.Add(propertyName, new PropertyInfoEx(propertyInfo));
                }
                else
                {
                    var fieldInfo = typeof(T).GetField(propertyName, Flags | BindingFlags.NonPublic);
                    Properties.Add(propertyName, new PropertyInfoEx(fieldInfo));
                }
            }

            return result;
        }
    }

    public class PropertyInfoEx
    {
        #region | Properties |

        public PropertyInfo PropertyInfo { get; private set; }

        public FieldInfo FieldInfo { get; private set; }

        public Type Type
        {
            get
            {
                return PropertyInfo == null ? FieldInfo.FieldType : PropertyInfo.PropertyType;
            }
        }

        public bool IsPublic { get; private set; }

        #endregion

        #region | Ctor |

        internal PropertyInfoEx(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                var exception = new ArgumentNullException("propertyInfo");
                Trace.TraceError(exception.Message);
                throw exception;
            }

            PropertyInfo = propertyInfo;

            IsPublic = propertyInfo.GetGetMethod() != null || propertyInfo.GetSetMethod() != null;
        }

        internal PropertyInfoEx(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                var exception = new ArgumentNullException("fieldInfo");
                Trace.TraceError(exception.Message);
                throw exception;
            }

            FieldInfo = fieldInfo;

            IsPublic = !fieldInfo.IsPrivate && fieldInfo.IsPublic;
        }

        #endregion

        #region | FastGetter |

        bool _getterInaccessible;
        private Func<object, object> _FastGetter;
        public Func<object, object> FastGetter
        {
            get
            {
                if (_FastGetter == null && !_getterInaccessible)
                {
                    //_FastGetter=PropertyInfo!=null
                    //    ?PropertyInfo.GetValueGetter<>
                }
                return _FastGetter;
            }
        }

        #endregion

        #region | FastSetter |

        #endregion
    }

    public static class PropertyInfoExtentions
    {
        public static Func<object, T> GetValueGetter<T>(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanRead || propertyInfo.GetGetMethod() == null) return null;

            var instance = Expression.Parameter(typeof(object), "i");
            var castedInstance = Expression.ConvertChecked(instance, propertyInfo.PropertyType);
            var argument = Expression.Parameter(typeof(T), "a");
            var property = Expression.Property(castedInstance, propertyInfo);
            var converter = Expression.Convert(property, typeof(T));

            var expression = Expression.Lambda(converter, instance);
            return (Func<object, T>)expression.Compile();
        }

        public static Action<object, T> GetValueSetter<T>(this PropertyInfo propertyInfo)
        {
            if (!propertyInfo.CanWrite || propertyInfo.GetSetMethod() == null) return null;

            var instance = Expression.Parameter(typeof(object), "i");
            var castedInstance = Expression.ConvertChecked(instance, propertyInfo.PropertyType);
            var argument = Expression.Parameter(typeof(T), "a");
            var property = Expression.Property(castedInstance, propertyInfo);
            var converter = Expression.Convert(property, typeof(T));
            return null;
            //return  Expression.Lambda<Action<object, T>>(converter, instance, argument)
        }
    }

    public static class TypeUtilities
    {
        #region | Properties |

        #region | PropertiesWithAttribute |

        static Dictionary<Tuple<Type, Type>, List<PropertyInfo>> _PropertiesWithAttribute = new Dictionary<Tuple<Type, Type>, List<PropertyInfo>>();

        public static List<PropertyInfo> GetPropertiesByAttribute(Type type, Type attributeType)
        {
            var tuple = new Tuple<Type, Type>(type, attributeType);
            if (!_PropertiesWithAttribute.Keys.Contains(tuple))
            {
                var properties = type.GetProperties(BindingFlags.CreateInstance | BindingFlags.Public | BindingFlags.Instance);
                var propertyInfos = properties
                    .Where(_ => _.CustomAttributes
                        .Any(_1 => _1.AttributeType == attributeType))
                        .ToList();
                _PropertiesWithAttribute.Add(tuple, propertyInfos);
            }

            return _PropertiesWithAttribute[tuple];
        }

        #endregion

        #endregion
    }
}
