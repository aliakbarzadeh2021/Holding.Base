using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Dtos;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.Infrastructure.Enums;

namespace Holding.Base.Sync.FileSyncer
{
    public class FileSyncService : IFileSyncService
    {

        private readonly IFileSyncRepository _fileSyncRepository;

        public FileSyncService(IFileSyncRepository fileSyncRepository)
        {
            _fileSyncRepository = fileSyncRepository;
        }

        public IQueryable<FileSync> GetFileSyncsAfterTime(DateTime? startDateTime)
        {
            if (startDateTime.HasValue)
            {
                return _fileSyncRepository.FindAll().Where(i => i.SyncedDateTime >= startDateTime.Value);
            }
            return new List<FileSync>().AsQueryable();
        }

        public FileSync GetLastFileSync()
        {
            if (_fileSyncRepository.FindAll().Any())
            {
                return _fileSyncRepository.FindAll()
                    .OrderBy(i => i.SyncedDateTime).LastOrDefault();
            }
            return null;
        }

        public void SaveFileSync(FileSync fileSync)
        {
            _fileSyncRepository.Save(fileSync);
        }

        public IEnumerable<IFileSyncInfo> GetFileSyncInfos(int count)
        {
            return _fileSyncRepository.FindAll()
                .OrderByDescending(i => i.SyncedDateTime)
                .Take(count)
                .Select(s => new Models.FileSyncInfo()
                {
                    Id = s.Id,
                    SyncState = (s.UploadErrors ?? new List<Guid>()).Count +
                                (s.DownloadErrors ?? new List<Guid>()).Count == 0 ?
                                SyncState.AllSuccess : SyncState.SomeError,
                    SuccessSyncFilesCount = s.DownloadFilesCount + s.UploadFilesCount -
                                (s.UploadErrors ?? new List<Guid>()).Count -
                                (s.DownloadErrors ?? new List<Guid>()).Count,
                    SyncFilesCount = s.DownloadFilesCount + s.UploadFilesCount,
                    SyncDateTime = s.SyncedDateTime,
                });
        }

        public IFileSyncDetails GetFileSyncDetails(Guid id)
        {
            var details = _fileSyncRepository.FindAll()
                .FirstOrDefault(c => c.Id == id);
            if (details == null)
                return null;
            return new FileSyncDetails()
            {
                UploadErrors = details.UploadErrors,
                DownloadErrors = details.DownloadErrors
            };
        }
    }
}
