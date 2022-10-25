using System;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.Role;

namespace Holding.Base.Event.Message.Role
{
    public class RoleDeletedEvent : EventBase, IRoleDeletedEvent
    {
        public RoleDeletedEvent(Guid roleId, Guid schoolId, string roleName)
        {
            RoleId = roleId;
            SchoolId = schoolId;
            RoleName = roleName;
        }

        public Guid RoleId { get; private set; }
        public Guid SchoolId { get; private set; }
        public string RoleName { get; private set; }
    }
}