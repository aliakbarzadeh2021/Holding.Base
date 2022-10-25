using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.Role
{
    public interface IPermissionEvent : IMessage
    {
        Guid Id { get; set; }
       
        string Name { get; }
        string BehaviorKey { get; }
        int DisplayOrder { get; }
        bool HasAccess { get; }
        Guid  ParentPermissionId { get; }
    }


}