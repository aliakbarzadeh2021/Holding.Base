using Holding.Base.Sync.Models;
using MongoDB.Bson.Serialization;

namespace Holding.Base.Sync.Repositories
{
    public class FileSyncRepository : Repository<FileSync>, IFileSyncRepository
    {
        public FileSyncRepository()
        {
            BsonClassMap.LookupClassMap(typeof(FileSync));
            _collection = _database.GetCollection<FileSync>(typeof(FileSync).Name);
        }

    }
}