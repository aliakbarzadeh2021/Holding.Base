using System.Collections.Generic;

namespace Holding.Base.Infrastructure.Messaging
{
    public interface IFileNames
    {
        IList<string> FileNames { get; set; }
    }
}