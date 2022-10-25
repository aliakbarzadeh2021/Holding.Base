using System.Collections.Generic;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.User
{
    public interface IUserRolesHasBeenChangedEvent : IMessage
    {
        string UserId { get; }
        IList<IUserRoleEvent> Roles { get; }
    }
}
