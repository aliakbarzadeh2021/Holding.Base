using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Holding.Base.Sync.Models
{
    public class FileSync
    {
        [Key]
        public Guid Id { get; set; }
        public int UploadFilesCount { get; set; }
        public int DownloadFilesCount { get; set; }
        public IList<Guid> DownloadErrors { get; set; }
        public IList<Guid> UploadErrors { get; set; }
        public DateTime SyncedDateTime { get; set; }


        public FileSync(Guid id, int uploadFilesCount, int downloadFilesCount,
            IList<Guid> downloadErrors, IList<Guid> uploadErrors,
            DateTime syncedDateTime)
        {
            Id = id;
            UploadFilesCount = uploadFilesCount;
            DownloadFilesCount = downloadFilesCount;
            DownloadErrors = downloadErrors;
            UploadErrors = uploadErrors;
            SyncedDateTime = syncedDateTime;
        }

        protected FileSync()
        {
        }
    }
    
}
