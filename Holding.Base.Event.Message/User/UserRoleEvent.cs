using System;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.User;

namespace Holding.Base.Event.Message.User
{
    public class UserRoleEvent :EventBase, IUserRoleEvent
    {
        public UserRoleEvent(Guid roleId, Guid schoolId, string roleName)
        {
            RoleId = roleId;
            RoleName = roleName;
            SchoolId = schoolId;
        }
        public Guid RoleId { get; private set; }
        public string RoleName { get; private set; }

        public Guid SchoolId { get; private set; }
    }
}
