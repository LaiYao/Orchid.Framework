using System;
using Orchid.Cloud.Logging;

namespace Microsoft.Extensions.Logging
{
    public static class KafkaLoggerFactoryExtensions
    {
        public static ILoggerFactory AddKafka(this ILoggerFactory factory, KafkaLoggerOptions options)
        {
            return factory.AddKafka(options, LogLevel.Information);
        }

        public static ILoggerFactory AddKafka(this ILoggerFactory factory, KafkaLoggerOptions options, LogLevel minLevel)
        {
            return factory.AddKafka(options, (string _, LogLevel logLevel) => logLevel >= minLevel);
        }

        public static ILoggerFactory AddKafka(this ILoggerFactory factory, KafkaLoggerOptions options, Func<string, LogLevel, bool> filter)
        {
            factory.AddProvider(new KafkaLoggerProvider(options, filter));
            return factory;
        }
    }
}
