using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holding.Base.Sync.Dtos
{
    public interface IFileSyncDetails
    {
        IEnumerable<Guid> UploadErrors { get; }
        IEnumerable<Guid> DownloadErrors { get; }
    }
}
