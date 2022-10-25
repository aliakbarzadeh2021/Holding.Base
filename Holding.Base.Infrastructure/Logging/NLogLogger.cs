//using System;
//using NLog;
//using NLog.Config;
//using NLog.Targets;
//using Holding.Base.Infrastructure.Messaging;

//namespace Holding.Base.Infrastructure.Logging
//{
//    public class NLogLogger : ILogger
//    {
//        private readonly Logger _logger;
//        public NLogLogger()
//        {
//            _logger = LogManager.GetLogger("Any");
//        }

//        public void Log(string message)
//        {
//            throw new NotImplementedException();
//        }

//        public void Info(string message)
//        {
//            _logger.Info(message);
//        }

//        public void Warn(string message)
//        {
//            _logger.Warn(message);
//        }

//        public void Debug(string message)
//        {
//            _logger.Debug(message);
//        }

//        public void Error(string message)
//        {
//            _logger.Error(message);
//        }

//        public void Error(Exception exception)
//        {
//            _logger.Error(exception);
//        }

//        public void Error(string message, Exception x)
//        {
//            _logger.Error(message);
//        }

//        public void Fatal(string message)
//        {
//            _logger.Fatal(message);
//        }

//        public void Fatal(Exception exception)
//        {
//            _logger.Fatal(exception);

//        }

//        /// <summary>
//        /// یک فرمان را لاگ میکند
//        /// </summary>
//        /// <param name="command">فرمان</param>
//        /// <param name="exception">خطا در صورتیکه اجرای فرمان با شکست مواجه شده باشد</param>
//        /// <param name="executionDuration">مدت زمان اجرای فرمان</param>
//        public void Command(ICommand command, Exception exception, TimeSpan executionDuration)
//        {
//            if (command == null)
//                return;
//            _logger.Info(command.GetLogMessage(), command, exception, executionDuration);
//        }
//    }

//}