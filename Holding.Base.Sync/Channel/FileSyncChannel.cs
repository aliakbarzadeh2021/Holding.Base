using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Holding.Base.Sync.FileSyncer;
using Holding.Base.Sync.Models;

namespace Holding.Base.Sync.Channel
{
    public class FileSyncChannel :
        ISyncFileInitializer,
        ISyncFileClientProvider,
        ISyncFileServerProvider,
        ISyncFileChecker,
        ISyncFileFinish
    {
        private readonly IFileSyncService _fileSyncService;
        private FileSync _lastFileSync;
        public IList<Guid> ServerFiles { get; private set; }
        public IFileSyncResult ClientFiles { get; private set; }
        public DateTime StartDateTime { get; private set; }

        private int _filesForUploadCount;
        private int _filesForDownloadCount;

        private FileSyncChannel(IWindsorContainer container)
        {
            _fileSyncService = container.Resolve<IFileSyncService>();
        }

        public ISyncFileFinish Process(out List<Guid> filesForUpload, out List<Guid> filesForDownload)
        {
            filesForUpload = ServerFiles.ToList();
            filesForDownload = ClientFiles.NotExist.ToList();

            _filesForUploadCount = filesForUpload.Count;
            _filesForDownloadCount = filesForDownload.Count;
            return this;
        }

        public ISyncFileServerProvider SetClientFiles(IClientFileReader clientFileReader)
        {
            ClientFiles = clientFileReader.GetClientFiles(StartDateTime);
            return this;
        }

        public ISyncFileClientProvider AutoInitialize()
        {
            _lastFileSync = _fileSyncService.GetLastFileSync();
            StartDateTime = _lastFileSync?.SyncedDateTime ?? new DateTime(1999, 1, 1);
            return this;
        }

        public ISyncFileClientProvider InitializeByDate(DateTime startDateTime)
        {
            StartDateTime = startDateTime;
            return this;
        }


        public ISyncFileChecker SetServerFiles(IServerFileReader serverFileReader)
        {
            ServerFiles = serverFileReader.GetServerFiles(StartDateTime, ClientFiles.Exist);
            return this;
        }


        public void Finish(IList<Guid> uploadErrors, IList<Guid> downloadErrors)
        {
            _fileSyncService.SaveFileSync(new FileSync(Guid.NewGuid(),
                _filesForUploadCount, _filesForDownloadCount,
                downloadErrors, uploadErrors,
                DateTime.Now));
        }

        public static ISyncFileInitializer Build(IWindsorContainer container)
        {
            var fileSync = new FileSyncChannel(container);
            return fileSync;
        }
    }


    public interface ISyncFileInitializer
    {
        DateTime StartDateTime { get; }
        ISyncFileClientProvider AutoInitialize();
        ISyncFileClientProvider InitializeByDate(DateTime startDateTime);
    }


    public interface IServerFileReader
    {
        IList<Guid> GetServerFiles(DateTime startDateTime, IList<Guid> clientExistFiles);
    }

    public interface IClientFileReader
    {
        IFileSyncResult GetClientFiles(DateTime startDateTime);
    }

    public interface ISyncFileServerProvider
    {
        DateTime StartDateTime { get; }
        IList<Guid> ServerFiles { get; }
        ISyncFileChecker SetServerFiles(IServerFileReader serverFileReader);
    }


    public interface ISyncFileClientProvider
    {
        DateTime StartDateTime { get; }
        IFileSyncResult ClientFiles { get; }
        ISyncFileServerProvider SetClientFiles(IClientFileReader clientFileReader);
    }


    public interface ISyncFileChecker
    {
        ISyncFileFinish Process(out List<Guid> filesForUpload, out List<Guid> filesForDownload);
    }

    public interface ISyncFileFinish
    {
        void Finish(IList<Guid> uploadErrors, IList<Guid> downloadErrors);
    }
}