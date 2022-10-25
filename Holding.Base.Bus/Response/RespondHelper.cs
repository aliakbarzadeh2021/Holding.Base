using System;
using System.Diagnostics;
using Holding.Base.Infrastructure.Messaging;
using Holding.Base.Sync.Configurations;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Models;
using MassTransit;


namespace Holding.Base.Bus.Response
{
    public static class RespondHelper
    {

        private static readonly ISyncApplicationSettings _syncSettings;


        static RespondHelper()
        {
            _syncSettings = SyncApplicationSettingsFactory.GetSolidApplicationSettings();
        }


/*

        private static void CreateSyncCommandPacket<TCommand>(TCommand command) where TCommand : CommandBase
        {
            var commandPacket = new RawDataPacket<TCommand>(command, _syncSettings.SyncHostVersion, DateTime.Now, _syncSettings.SyncHostType);
            var entity = RawDataPacketRepository<TCommand>.FindBy(commandPacket.Id);
            if (entity != null)
            {
                RawDataPacketRepository<TCommand>.Remove(entity.Id);
            }
            RawDataPacketRepository<TCommand>.Add(commandPacket);
        }

*/


        public static void RespondExtend(this IConsumeContext cx, ResponseCommand responseCommand, CommandBase command)
        {

            /*if (_syncSettings.IsSyncActive && responseCommand.Success && !command.ToSync)
            {
                CreateSyncCommandPacket(command);
            }*/


            if (cx.ResponseAddress != null)
            {
                cx.Respond(responseCommand);
            }
        }



        public static void RespondExtend(this IConsumeContext cx, ResponseCommand responseCommand, CommandBase command, Exception exception)
        {

            /*if (_syncSettings.IsSyncActive && responseCommand.Success && !command.ToSync)
            {
                CreateSyncCommandPacket(command);
            }*/


            if (cx.ResponseAddress != null)
            {
                cx.Respond(responseCommand);
            }
            else
            {
                throw exception;
            }

        }



    }
}

