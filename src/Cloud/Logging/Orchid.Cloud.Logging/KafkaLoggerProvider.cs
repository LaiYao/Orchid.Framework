using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace Orchid.Cloud.Logging
{
    public class KafkaLoggerProvider : ILoggerProvider
    {
        #region | Fields |

        Func<string, LogLevel, bool> _filter;
        Producer<Null, string> _producer;

        #endregion

        public KafkaLoggerProvider(KafkaLoggerOptions options, Func<string, LogLevel, bool> filter)
        {
            _filter = filter;

            var config = new Dictionary<string, object>
            {
                { "group.id", "logging" },
                { "bootstrap.servers", options.BrokerUrls }
            };
            _producer = new Producer<Null, string>(config, null, new StringSerializer(Encoding.UTF8));
        }

        public KafkaLoggerProvider(KafkaLoggerOptions options, LogLevel minLevel)
            : this(options, (_, __) => __ >= minLevel)
        {
        }

        #region | Implement of ILoggerProvider |

        public ILogger CreateLogger(string categoryName)
        {
            return new KafkaLogger(categoryName, _producer, _filter);
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        #endregion

        #region | Helpers |

        void Dispose(bool disposing)
        {
            if (!disposing) return;

            _producer?.Dispose();
            _producer = null;
        }

        #endregion
    }
}
