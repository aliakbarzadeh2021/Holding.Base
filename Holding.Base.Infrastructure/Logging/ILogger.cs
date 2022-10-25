using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Holding.Base.Infrastructure.Logging
{
    public interface ILogger
    {
        void Log(string message);
        void Info(string message);
        void Warn(string message);
        void Debug(string message);
        void Error(string message);
        void Error(Exception x);
        void Error(string message, Exception x);
        void Fatal(string message);
        void Fatal(Exception x);
    }
}
