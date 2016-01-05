//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading.Tasks;

//namespace Orchid.Repo.EF
//{
//    public class DefaultDbInterception : DbCommandInterceptor
//    {
//        private readonly Stopwatch _stopwatch = new Stopwatch();

//        public override void ScalarExecuting(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
//        {
//            base.ScalarExecuting(command, interceptionContext);
//            _stopwatch.Restart();
//        }

//        public override void ScalarExecuted(DbCommand command, DbCommandInterceptionContext<object> interceptionContext)
//        {
//            _stopwatch.Stop();
//            if (interceptionContext.Exception != null)
//            {
//                Trace.TraceError("Exception:{1} \r\n --> Error executing command: {0}", command.CommandText, interceptionContext.Exception.ToString());
//            }
//            else
//            {
//                Trace.TraceInformation("\r\n执行时间:{0} 毫秒\r\n-->ScalarExecuted.Command:{1}\r\n", _stopwatch.ElapsedMilliseconds, command.CommandText);
//            }
//            base.ScalarExecuted(command, interceptionContext);
//        }

//        public override void NonQueryExecuting(DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
//        {
//            base.NonQueryExecuting(command, interceptionContext);
//            _stopwatch.Restart();
//        }

//        public override void NonQueryExecuted(System.Data.Common.DbCommand command, DbCommandInterceptionContext<int> interceptionContext)
//        {
//            _stopwatch.Stop();
//            if (interceptionContext.Exception != null)
//            {
//                Trace.TraceError("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString());
//            }
//            else
//            {
//                Trace.TraceInformation("\r\n执行时间:{0} 毫秒\r\n-->NonQueryExecuted.Command:\r\n{1}", _stopwatch.ElapsedMilliseconds, command.CommandText);
//            }
//            base.NonQueryExecuted(command, interceptionContext);
//        }

//        public override void ReaderExecuting(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
//        {
//            base.ReaderExecuting(command, interceptionContext);
//            _stopwatch.Restart();
//        }

//        public override void ReaderExecuted(DbCommand command, DbCommandInterceptionContext<DbDataReader> interceptionContext)
//        {
//            _stopwatch.Stop();
//            if (interceptionContext.Exception != null)
//            {
//                Trace.TraceError("Exception:{1} \r\n --> Error executing command:\r\n {0}", command.CommandText, interceptionContext.Exception.ToString());
//            }
//            else
//            {
//                Trace.TraceInformation("\r\n执行时间:{0} 毫秒 \r\n -->ReaderExecuted.Command:\r\n{1}", _stopwatch.ElapsedMilliseconds, command.CommandText);
//            }
//            base.ReaderExecuted(command, interceptionContext);
//        }
//    }
//}
