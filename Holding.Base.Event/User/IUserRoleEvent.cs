using System;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.User
{
    public interface IUserRoleEvent : IMessage
    {
        Guid RoleId { get; }
        string RoleName { get; }
        Guid SchoolId { get; }

    }
}
