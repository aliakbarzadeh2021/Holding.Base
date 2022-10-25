using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holding.Base.Sync.FileSyncer
{
    public interface IFileSyncResolver
    {
        IEnumerable<Guid> Resolve();
    }

}
