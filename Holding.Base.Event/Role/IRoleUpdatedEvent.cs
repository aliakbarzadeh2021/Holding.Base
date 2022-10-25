using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.Role
{
    public interface IRoleUpdatedEvent : IMessage
    {
        Guid RoleId { get; }
        string Name { get; }
        Guid SchoolId { get; }
    }
}
