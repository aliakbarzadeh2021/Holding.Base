using System;
using System.Collections.Generic;
using Holding.Base.Sync.Dtos;
using Holding.Base.Sync.FileSyncer;
using Holding.Base.Sync.Service;

namespace Holding.Base.Sync.FileSyncInfo
{
    public class FileSyncInfoService : IFileSyncInfoService
    {
        private readonly IFileSyncService _fileSyncService;

        public FileSyncInfoService(IFileSyncService fileSyncService)
        {
            _fileSyncService = fileSyncService;
        }

        public IEnumerable<IFileSyncInfo> GetFileSyncInfos(int count)
        {
            return _fileSyncService.GetFileSyncInfos(count);
        }

        public IFileSyncDetails GetFileSyncDetails(Guid id)
        {
            return _fileSyncService.GetFileSyncDetails(id);
        }
    }
}
