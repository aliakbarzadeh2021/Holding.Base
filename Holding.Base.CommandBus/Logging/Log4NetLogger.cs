using System;
using log4net;
using Holding.Base.Infrastructure.Logging;

namespace Holding.Base.CommandBus.Logging
{
    public class Log4NetLogger : ILogger
    {
        private readonly ILog _log;
        public Log4NetLogger()
        {
            this._log = LogManager.GetLogger(this.GetType());
        }

        public void Log(string message)
        {
            throw new NotImplementedException();
        }

        public void Info(string message)
        {
            _log.Info(message);
        }

        public void Warn(string message)
        {
            _log.Warn(message);
        }

        public void Debug(string message)
        {
            _log.Debug(message);
        }

        public void Error(string message)
        {
            _log.Error(message);
        }

        public void Error(Exception exception)
        {
            _log.Error(exception);
        }

        public void Error(string message, Exception exception)
        {
            _log.Error(message, exception);
        }

        public void Fatal(string message)
        {
            _log.Fatal(message);
        }

        public void Fatal(Exception exception)
        {
            _log.Fatal(exception);
        }
    }

}