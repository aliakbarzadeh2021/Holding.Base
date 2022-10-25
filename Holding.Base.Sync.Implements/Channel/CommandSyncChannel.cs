using System;
using System.Collections.Generic;
using System.Linq;
using Holding.Base.Sync.Channel;
using Holding.Base.Sync.CommandSyncService;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Exceptions;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Implements.Channel
{
    internal class CommandSyncChannel<TCommand> : ICommandSyncChannel<TCommand> where TCommand : ICommand
    {
        
        private readonly ISyncStepRepository<TCommand> _syncStepRepository;
        private readonly ISyncCommandFilter<TCommand> _syncCommandFilter;
        private readonly ISyncAnCommandWithServer<TCommand> _syncAnCommandWithServer;
        private readonly ISyncAnCommandWithClient<TCommand> _syncAnCommandWithClient;
        private bool _isInitialize;
        private SyncStep<TCommand>  _syncStep;
        private List<SyncDataPacket<TCommand>> _serverSyncDataPackets;
        private List<SyncDataPacket<TCommand>> _clientSyncDataPackets;


        public CommandSyncChannel(ISyncStepRepository<TCommand> syncStepRepository, ISyncCommandFilter<TCommand> syncCommandFilter, ISyncAnCommandWithServer<TCommand> syncAnCommandWithServer, ISyncAnCommandWithClient<TCommand> syncAnCommandWithClient)
        {
            _syncStepRepository = syncStepRepository;
            _syncCommandFilter = syncCommandFilter;
            _syncAnCommandWithServer = syncAnCommandWithServer;
            _syncAnCommandWithClient = syncAnCommandWithClient;
            
            _serverSyncDataPackets=null;
            _clientSyncDataPackets = null;

            _isInitialize = false;
        }


        public void Initialize(SyncStep<TCommand> syncStep)
        {
            if (syncStep == null)
            {
                throw new Exception("سینک ستپ شما خالی است");
            }
            _syncStep = syncStep;
            _isInitialize = true;
        }
        

        public void SetSyncCommandPackets(IList<RawDataPacket<TCommand>> clientRawDataPackets, IList<RawDataPacket<TCommand>> serverRawDataPackets)
        {
            if (!_syncStep.IsSyncStepFiltered)
            {
                _syncStep.SyncDataPackets = new List<SyncDataPacket<TCommand>>();

                foreach (var clientPacket in clientRawDataPackets)
                {
                    SyncDataPacket<TCommand> t=new SyncDataPacket<TCommand>(clientPacket.Command,clientPacket.HostVersion, clientPacket.CrationDateTime, HostType.Client, SyncResult.None,SyncLife.None);
                    _syncStep.SyncDataPackets.Add(t);
                }

                foreach (var serverPacket in serverRawDataPackets)
                {
                    SyncDataPacket<TCommand> t = new SyncDataPacket<TCommand>(serverPacket.Command, serverPacket.HostVersion, serverPacket.CrationDateTime, HostType.Server, SyncResult.None, SyncLife.None);
                    _syncStep.SyncDataPackets.Add(t);
                }

                _syncCommandFilter.Filter(_syncStep.SyncDataPackets);
                _syncStep.IsSyncStepFiltered = true;

                SaveSyncStep();
            }
            else
            {
                throw new AlreadySyncStepIsFilteredException();
            }

        }


        public void SeparateCommandPackets()
        {
            if (!_syncStep.IsSyncStepFiltered)
            {
                throw new Exception("سینک استپ ابتدا باید فیلتر شده باشد");
            }

            currentClientCommand = 0;

            currentServerCommand = 0;

            _serverSyncDataPackets= _syncStep.SyncDataPackets.Where(i => i.Syncable && i.SyncLife != SyncLife.EndSync && i.HostType == HostType.Server)
                .OrderBy(d => d.CrationDateTime).ToList();

            _clientSyncDataPackets = _syncStep.SyncDataPackets.Where(i => i.Syncable && i.SyncLife != SyncLife.EndSync && i.HostType == HostType.Client)
                     .OrderBy(d => d.CrationDateTime).ToList();


        }

        
        public bool SyncStepFiltered { get { return _syncStep.IsSyncStepFiltered; } }

        public int ServerSyncDataPacketsCount
        {
            get
            {
                if (_serverSyncDataPackets==null)
                {
                    throw new Exception("ابتدا مرحله جدا سازی انجام گردد");
                }
                return _serverSyncDataPackets.Count;
            }
        }

        public int ClientSyncDataPacketsCount
        {
            get
            {
                    if (_serverSyncDataPackets == null)
                    {
                        throw new Exception("ابتدا مرحله جدا سازی انجام گردد");
                    }
                    return _clientSyncDataPackets.Count;
                }
            }



        

        private int currentServerCommand;
        public bool NextSyncOfServerCommand()
        {
            if (ServerSyncDataPacketsCount> currentServerCommand)
            {
                _syncAnCommandWithClient.Sync(_serverSyncDataPackets[currentServerCommand]);
                SaveSyncStep();
                currentServerCommand++;
            }

            return ServerSyncDataPacketsCount > currentServerCommand;
        }

        private int currentClientCommand;
        public bool NextSyncOfClientCommand()
        {
            if (ClientSyncDataPacketsCount > currentClientCommand)
            {
                _syncAnCommandWithServer.Sync(_clientSyncDataPackets[currentClientCommand]);
                SaveSyncStep();
                currentClientCommand++;
            }

            return ClientSyncDataPacketsCount > currentClientCommand;

        }



        private void SaveSyncStep()
        {
            _syncStepRepository.Save(_syncStep);
        }





    }
}
