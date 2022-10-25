using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Holding.Base.Sync.Dtos;

namespace Holding.Base.Sync.Models
{
    public class FileSyncDetails : IFileSyncDetails
    {
        public FileSyncDetails()
        {
            UploadErrors = new List<Guid>();
            DownloadErrors = new List<Guid>();
        }

        public IEnumerable<Guid> UploadErrors { get; set; }
        public IEnumerable<Guid> DownloadErrors { get; set; }

    }

}
