using Castle.Windsor;
using Holding.Base.Sync.Configurations;
using Holding.Base.Sync.Models;
using Holding.Base.Sync.Repositories;
using Holding.Base.CommandBus.Implements;
using Holding.Base.CommandBus.Messages;
using Holding.Base.Infrastructure.Events.SyncEvent;

namespace Holding.Base.Sync.Implements.Installer
{
    public class KernelSyncListenerInstaller<TCommand> where TCommand : ICommand
    {
        private readonly SyncRawDataEventListener _listener;
        private IRawDataPacketRepository<TCommand> _rawDataPacketRepository;
        
        public KernelSyncListenerInstaller()
        {
            _listener=new SyncRawDataEventListener();
        }

        public void Install(SyncableDefaultBus bus, IWindsorContainer container)
        {
            _rawDataPacketRepository = container.Resolve<IRawDataPacketRepository<TCommand>>();
            _listener.Subscribe(bus, AddRawDataPacket);
        }


        public void AddRawDataPacket(SyncRawDataEventArgs args)
        {
            var command = (TCommand)args.Value;
            var commandPacket = new RawDataPacket<TCommand>(command, SyncApplicationSettingsFactory.GetApplicationSettings().SyncHostVersion, args.DateTime);
            var entity = _rawDataPacketRepository.FindBy(commandPacket.Id);
            if (entity != null)
            {
                _rawDataPacketRepository.Remove(entity.Id);
            }
            _rawDataPacketRepository.Add(commandPacket);
        }

    }
}