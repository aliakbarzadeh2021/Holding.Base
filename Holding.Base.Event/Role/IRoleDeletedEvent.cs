using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.Role
{
    public interface IRoleDeletedEvent : IMessage
    {
        Guid RoleId { get; }

        Guid SchoolId { get; }
        string RoleName { get; }
    }
}
