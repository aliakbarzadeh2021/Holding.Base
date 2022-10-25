using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.Role
{
    public interface IRoleCreatedEvent : IMessage
    {
        Guid RoleId { get; }
        string Name { get; }
        Guid SchoolId { get; }
    }
}
