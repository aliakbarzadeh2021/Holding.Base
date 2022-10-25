using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holding.Base.Sync.FileSyncer
{
    public interface IFileSyncResult
    {
        IList<Guid> Exist { get; set; }
        IList<Guid> NotExist { get; set; }
    }

    internal class FileSyncResult: IFileSyncResult
    {
        public IList<Guid> Exist { get; set; }
        public IList<Guid> NotExist { get; set; }
    }
}
