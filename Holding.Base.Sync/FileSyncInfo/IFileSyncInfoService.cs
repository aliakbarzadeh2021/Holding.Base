using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Holding.Base.Sync.Dtos;

namespace Holding.Base.Sync.FileSyncInfo
{
    public interface IFileSyncInfoService
    {
        IEnumerable<IFileSyncInfo> GetFileSyncInfos(int count);
        IFileSyncDetails GetFileSyncDetails(Guid id);
    }
}
