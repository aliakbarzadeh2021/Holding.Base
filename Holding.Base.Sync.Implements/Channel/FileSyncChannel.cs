using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Holding.Base.Sync.Channel;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.FileSyncService;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.CommandBus.Messages;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Sync.Implements.Channel
{
    internal class FileSyncChannel<TCommand> : IFileSyncChannel<TCommand> where TCommand : ICommand
    {

        private SyncStep<TCommand> _syncStep;
        private readonly ISyncStepRepository<TCommand> _syncStepRepository;
        private bool isSyncFilesPrepare;
        private readonly ICheckFilesWithOtherHost _checkFilesWithServer;
        private readonly ICheckFilesWithOtherHost _checkFilesWithClient;
        private readonly IPostFileToServer _postFileToServer;
        private readonly IGetFileFromServer _getFileFromServer;


        public FileSyncChannel(ICheckFilesWithOtherHost checkFilesWithServer, ICheckFilesWithOtherHost checkFilesWithClient,IPostFileToServer postFileToServer,IGetFileFromServer getFileFromServer, ISyncStepRepository<TCommand> syncStepRepository)
        {
            _checkFilesWithServer = checkFilesWithServer;
            _checkFilesWithClient = checkFilesWithClient;
            _postFileToServer = postFileToServer;
            _getFileFromServer = getFileFromServer;
            _syncStepRepository = syncStepRepository;
            isSyncFilesPrepare = false;
        }
        

        public async Task SyncFilesPrepare(SyncVersion syncVersion)
        {
            _syncStep= _syncStepRepository.FindAll().SingleOrDefault(i => i.SyncVersion == syncVersion);

            if (_syncStep==null)
            {
                throw new Exception("برای این ورژن سینکی ثبت نشده است");
            }

            if (!_syncStep.IsCommandSyncCompeleted)
            {
                throw new Exception("ابتدا سینک داده ای را انجام دهید");
            }

            if (!_syncStep.IsFileSyncStarted)
            {
                FileNamesForSendToServer = await _checkFilesWithServer.Check(getFileNameByHost(HostType.Client));
                FileNamesForGetFromServer = await _checkFilesWithClient.Check(getFileNameByHost(HostType.Server));

                _syncStep.FileSyncItems=new List<FileSyncItem>();
                AddFileToFileSyncStep(FileNamesForSendToServer,HostType.Client,HostType.Server);
                AddFileToFileSyncStep(FileNamesForGetFromServer,HostType.Server, HostType.Client);
                _syncStep.IsFileSyncStarted = true;
                _syncStepRepository.Save(_syncStep);
            }
            else
            {
                FileNamesForSendToServer= _syncStep.FileSyncItems.Where(i => i.SourceHostType == HostType.Client && !i.IsSync).Select(i => i.FileName).ToList();
                FileNamesForGetFromServer= _syncStep.FileSyncItems.Where(i => i.SourceHostType == HostType.Server && !i.IsSync).Select(i=>i.FileName).ToList();
            }
            
            isSyncFilesPrepare = true;
        }


        private void AddFileToFileSyncStep(IList<string> fileNames, HostType src, HostType dest)
        {
            foreach (var item in fileNames)
            {
                FileSyncItem fileSyncItem = new FileSyncItem
                {
                    IsSync = false,
                    SourceHostType = src,
                    DestinationHostType = dest,
                    FileName = item
                };
                _syncStep.FileSyncItems.Add(fileSyncItem);
            }
        }


        public IList<string> FileNamesForSendToServer { get; private set; }
        

        public IList<string> FileNamesForGetFromServer { get; private set; }
        

        private IList<string> getFileNameByHost(HostType hostType)
        {
            var result = new List<string>();
            var _commandPackets =
                _syncStep.SyncDataPackets
                    .Where(i => i.HostType== hostType)
                    .ToList();

            if (_commandPackets != null)
            {
                foreach (var command in _commandPackets)
                {
                    var fileName = command as IFileName;
                    if (fileName != null)
                    {
                        result.Add(fileName.FileName);
                    }
                    else
                    {
                        var fileNames = command as IFileNames;
                        if (fileNames != null)
                        {
                            result.AddRange(fileNames.FileNames);
                        }
                    }
                }
            }
            return result;
        }
        

        public async Task<bool> PostFileToServer(string fileName)
        {
            if (!isSyncFilesPrepare)
            {
                throw new Exception("ابتدا سینک فایل خود را آماده کنید");
            }

            var syncFile = _syncStep.FileSyncItems.FirstOrDefault(i => i.FileName == fileName);
            if (syncFile==null)
            {
                throw new Exception("فایل سینکی با این نام یافت نشد");
            }
            if (syncFile.IsSync)
            {
                throw new Exception("این فایل قبلا سینک شده است");
            }
            if (syncFile.SourceHostType!=HostType.Client)
            {
                throw new Exception("این فایل باید از سرور دریافت شود");
            }

            var resultOfPost =await _postFileToServer.Post(fileName);
            if (resultOfPost)
            {
                syncFile.IsSync = true;
                _syncStepRepository.Save(_syncStep);
                return true;
            }

            return false;
        }


        public async Task<bool> GetFileFromServer(string fileName)
        {
            if (!isSyncFilesPrepare)
            {
                throw new Exception("ابتدا سینک فایل خود را آماده کنید");
            }

            var syncFile = _syncStep.FileSyncItems.FirstOrDefault(i => i.FileName == fileName);
            if (syncFile == null)
            {
                throw new Exception("فایل سینکی با این نام یافت نشد");
            }
            if (syncFile.IsSync)
            {
                throw new Exception("این فایل قبلا سینک شده است");
            }
            if (syncFile.SourceHostType != HostType.Server)
            {
                throw new Exception("این فایل باید به سرور ارسال شود");
            }

            var resultOfGet = await _getFileFromServer.Get(fileName);
            if (resultOfGet)
            {
                syncFile.IsSync = true;
                _syncStepRepository.Save(_syncStep);
                return true;
            }

            return false;
        }

    }
}
