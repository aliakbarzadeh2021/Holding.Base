using System.Collections.Generic;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.User;

namespace Holding.Base.Event.Message.User
{
    public class UserRolesHasBeenChangedEvent : EventBase , IUserRolesHasBeenChangedEvent
    {
        public UserRolesHasBeenChangedEvent( string userId  , IList<IUserRoleEvent> roles )
        {
            UserId = userId;
            Roles = roles;            
        }
        public string UserId { get; private set; }        
        public IList<IUserRoleEvent> Roles { get; private set; }
    }
}
