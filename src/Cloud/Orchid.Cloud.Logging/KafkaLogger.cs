using Orchid.Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Orchid.Cloud.Logging
{
    public class KafkaLogger : ILogger
    {
        #region | Fields |

        string _name;
        Producer<Null, string> _producer;
        Func<string, LogLevel, bool> _filter;

        #endregion

        #region | Ctor |

        public KafkaLogger(string name, Producer<Null, string> producer, Func<string, LogLevel, bool> filter)
        {
            Check.NotNull(name, nameof(name));
            Check.NotNull(producer, nameof(producer));
            Check.NotNull(filter, nameof(filter));

            _name = name;
            _producer = producer;
            _filter = filter;
        }

        #endregion

        #region | Implement of ILogger |

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = formatter == null ? state.ToString() : formatter.Invoke(state, exception);
            if (string.IsNullOrEmpty(message))
            {
                return;
            }

            message = $"{logLevel}: {message}";
            _producer.ProduceAsync(_name, null, message);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return _filter == null || _filter(_name, logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return _producer;
        }

        #endregion
    }
}
