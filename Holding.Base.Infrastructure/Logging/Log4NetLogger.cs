using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Holding.Base.Infrastructure.Configuration;
using log4net;
using log4net.Config;

namespace Holding.Base.Infrastructure.Logging
{
    public class Log4NetLogger : ILogger
    {

        private readonly ILog _logger;

        public Log4NetLogger()
        {
            _logger = LogManager.GetLogger(this.GetType());
            XmlConfigurator.Configure();
        }

        public void Info(string message)
        {
            _logger.Info(message);
        }

        public void Warn(string message)
        {
            _logger.Warn(message);
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception x)
        {
            Error(LogUtility.BuildExceptionMessage(x));
        }

        public void Error(string message, Exception x)
        {
            _logger.Error(message, x);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception x)
        {
            Fatal(LogUtility.BuildExceptionMessage(x));
        }

        public void Log(string message)
        {
            _logger.Info(message);
        }
    }

}
