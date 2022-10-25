using System;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.Role;

namespace Holding.Base.Event.Message.Role
{
    public class RoleUpdatedEvent :EventBase, IRoleUpdatedEvent
    {
        public RoleUpdatedEvent(Guid roleId, string name, Guid schoolId)
        {
            RoleId = roleId;
            Name = name;
            SchoolId = schoolId;
        }

        public Guid RoleId { get; private set; }
        public string Name { get; private set; }
        public Guid SchoolId { get; private set; }
    }
}