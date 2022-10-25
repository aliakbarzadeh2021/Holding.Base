using System;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.Role;

namespace Holding.Base.Event.Message.Role
{
    public class PermissionEvent : EventBase , IPermissionEvent
    {
        public PermissionEvent(Guid id, string name, string behaviorKey, int displayOrder, bool hasAccess, Guid parentPermissionId)
        {
            Id = id;
            Name = name;
            BehaviorKey = behaviorKey;
            DisplayOrder = displayOrder;
            HasAccess = hasAccess;
            ParentPermissionId = parentPermissionId;
        }

        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string BehaviorKey { get; private set; }
        public int DisplayOrder { get; private set; }
        public bool HasAccess { get; private set; }
        public Guid ParentPermissionId { get; private set; }
    }
}