using System;
using System.Collections.Generic;
using System.Linq;
using Castle.Windsor;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.Sync.Strategy;
using Holding.Base.CommandBus.Messages;

namespace Holding.Base.Sync.Channel
{
    public class CommandSyncChannel<TCommand> :
        ISyncStepInitializer<TCommand>,
        ISyncRawDataPacketsInitializer<TCommand>,
        ISyncDataPacketPrepare<TCommand>,
        ISyncFilter<TCommand>,
        ISyncDataPacketsPrepareForServerAndClient<TCommand>,
        ISyncFinish<TCommand>
        where TCommand : ICommand
    {
        private readonly ISyncStepRepository<TCommand> _syncStepRepository;
        private IEnumerable<RawDataPacket<TCommand>> _clientRawDataPackets;
        private IEnumerable<RawDataPacket<TCommand>> _serverRawDataPackets;
        private SyncStep<TCommand> _syncStep;

        private CommandSyncChannel(IWindsorContainer container)
        {
            _syncStepRepository = container.Resolve<ISyncStepRepository<TCommand>>();
            //_syncDataPacketRepository = container.Resolve<ISyncDataPacketRepository<TCommand>>();
        }

        public IEnumerable<SyncDataPacket<TCommand>> SyncDataPacketsForSendToClient
        {
            get
            {
                if (_syncStep != null && _syncStep.FilteredSyncDataPackets != null)
                    return _syncStep.FilteredSyncDataPackets.Where(i => i.HostType == HostType.Server);
                return new List<SyncDataPacket<TCommand>>();
            }
        }


        public IEnumerable<SyncDataPacket<TCommand>> SyncDataPacketsForSendToServer
        {
            get
            {
                if (_syncStep != null && _syncStep.FilteredSyncDataPackets != null)
                    return _syncStep.FilteredSyncDataPackets.Where(i => i.HostType == HostType.Client);
                return new List<SyncDataPacket<TCommand>>();
            }
        }

        public ISyncFilter<TCommand> PrepareSyncDataPackets()
        {
            if (!_syncStep.ExistClientSyncDataPackets)
                foreach (var clientRawDataPacket in _clientRawDataPackets)
                    _syncStep.ClientSyncDataPackets.Add(new SyncDataPacket<TCommand>(Guid.NewGuid(),
                        clientRawDataPacket.Command,
                        DateTime.Now, HostType.Client)
                    {
                        CommandName = clientRawDataPacket.CommandName,
                        CommandType = clientRawDataPacket.CommandType,
                        SerializedCommand = clientRawDataPacket.SerializedCommand
                    });

            if (!_syncStep.ExistServerSyncDataPackets)
                foreach (var serverRawDataPacket in _serverRawDataPackets)
                    _syncStep.ServerSyncDataPackets.Add(new SyncDataPacket<TCommand>(Guid.NewGuid(),
                        serverRawDataPacket.Command,
                        DateTime.Now, HostType.Server)
                    {
                        CommandName = serverRawDataPacket.CommandName,
                        CommandType = serverRawDataPacket.CommandType,
                        SerializedCommand = serverRawDataPacket.SerializedCommand
                    });

            _syncStepRepository.Save(_syncStep);

            return this;
        }


        public ISyncFinish<TCommand> PreparePacketForServerAndClient(
            out IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsForSendToClient,
            out IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsForSendToServer)
        {
            if (_syncStep != null && _syncStep.FilteredSyncDataPackets != null)
            {
                syncDataPacketsForSendToClient =
                    _syncStep.FilteredSyncDataPackets.Where(
                            i => i.HostType == HostType.Server && i.SyncResult != SyncResult.Success)
                        .OrderBy(i => i.CrationDateTime).ToList();
                syncDataPacketsForSendToServer =
                    _syncStep.FilteredSyncDataPackets.Where(
                            i => i.HostType == HostType.Client && i.SyncResult != SyncResult.Success)
                        .OrderBy(i => i.CrationDateTime).ToList();
            }
            else
            {
                syncDataPacketsForSendToClient = new List<SyncDataPacket<TCommand>>();
                syncDataPacketsForSendToServer = new List<SyncDataPacket<TCommand>>();
            }


            return this;
        }


        public ISyncDataPacketsPrepareForServerAndClient<TCommand> Filter(IFilterStrategy<TCommand> filterStrategy)
        {
            filterStrategy.Filter(_syncStep);

            foreach (var filteredSyncDataPacket in _syncStep.FilteredSyncDataPackets)
                filteredSyncDataPacket.SyncLife = SyncLife.StartSync;

            _syncStep.IsSyncStepFiltered = true;

            _syncStepRepository.Save(_syncStep);

            return this;
        }


        public void Finish(IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsForSendToClient,
            IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsForSendToServer)
        {
            foreach (var syncDataPacket in syncDataPacketsForSendToClient.Union(syncDataPacketsForSendToServer))
            {
                var dataPack = _syncStep.FilteredSyncDataPackets.FirstOrDefault(i => i.Id == syncDataPacket.Id);
                if (dataPack != null)
                {
                    dataPack.SyncLife = SyncLife.EndSync;
                    dataPack.SyncDate = DateTime.Now;
                    dataPack.SyncError = syncDataPacket.SyncError;
                    dataPack.SyncException = syncDataPacket.SyncException;
                    dataPack.SyncResult = syncDataPacket.SyncResult;
                    dataPack.IsSync = syncDataPacket.SyncResult == SyncResult.Success;
                }
            }
            _syncStep.IsSyncCompeleted = true;
            _syncStepRepository.Save(_syncStep);
        }


        public ISyncDataPacketPrepare<TCommand> InitializeRawDataPackets(
            IEnumerable<RawDataPacket<TCommand>> clientRawDataPackets,
            IEnumerable<RawDataPacket<TCommand>> serverRawDataPackets)
        {
            _clientRawDataPackets = clientRawDataPackets;
            _serverRawDataPackets = serverRawDataPackets;

            return this;
        }


        public ISyncRawDataPacketsInitializer<TCommand> InitializeSyncStep(SyncVersion syncVersion)
        {
            var syncStep =
                _syncStepRepository.FindAll().SingleOrDefault(i => i.SyncVersion.Version == syncVersion.Version);
            if (syncStep == null)
            {
                syncStep = new SyncStep<TCommand>
                {
                    Id = Guid.NewGuid(),
                    SyncVersion = syncVersion,
                    IsSyncCompeleted = false,
                    IsSyncStepFiltered = false,
                    ServerSyncDataPackets = new List<SyncDataPacket<TCommand>>(),
                    ClientSyncDataPackets = new List<SyncDataPacket<TCommand>>(),
                    FilteredSyncDataPackets = new List<SyncDataPacket<TCommand>>()
                };
                _syncStepRepository.Add(syncStep);
            }

            _syncStep = syncStep;

            return this;
        }


        public static ISyncStepInitializer<TCommand> Build(IWindsorContainer container)
        {
            var commandSyncChannel = new CommandSyncChannel<TCommand>(container);
            return commandSyncChannel;
        }
    }


    public interface ISyncStepInitializer<TCommand> where TCommand : ICommand
    {
        ISyncRawDataPacketsInitializer<TCommand> InitializeSyncStep(SyncVersion syncStep);
    }


    public interface ISyncRawDataPacketsInitializer<TCommand> where TCommand : ICommand
    {
        ISyncDataPacketPrepare<TCommand> InitializeRawDataPackets(
            IEnumerable<RawDataPacket<TCommand>> clientRawDataPackets,
            IEnumerable<RawDataPacket<TCommand>> serverRawDataPackets);
    }


    public interface ISyncDataPacketPrepare<TCommand> where TCommand : ICommand
    {
        ISyncFilter<TCommand> PrepareSyncDataPackets();
    }


    public interface ISyncFilter<TCommand> where TCommand : ICommand
    {
        ISyncDataPacketsPrepareForServerAndClient<TCommand> Filter(IFilterStrategy<TCommand> filterStrategy);
    }


    public interface ISyncDataPacketsPrepareForServerAndClient<TCommand> where TCommand : ICommand
    {
        /*IEnumerable<SyncDataPacket<TCommand>> SyncDataPacketsForSendToClient{ get; }
        IEnumerable<SyncDataPacket<TCommand>> SyncDataPacketsForSendToServer{ get; }*/

        ISyncFinish<TCommand> PreparePacketForServerAndClient(
            out IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsForSendToClient,
            out IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsForSendToServer);
    }


    public interface ISyncFinish<TCommand> where TCommand : ICommand
    {
        void Finish(IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsReceiveFromClient,
            IEnumerable<SyncDataPacket<TCommand>> syncDataPacketsReceiveFromServer);
    }
}