using System;
using System.Collections.Generic;
using Holding.Base.Event.Message.Core;
using Holding.Base.Event.Role;

namespace Holding.Base.Event.Message.Role
{
    public class RolePermissionHasBeenSetEvent : EventBase , IRolePermissionHasBeenSetEvent
    {
        public RolePermissionHasBeenSetEvent( string roleName, Guid roleInSchoolId, Guid schoolId, List<IPermissionEvent> permission)
        {
            RoleInSchoolId = roleInSchoolId;
            RoleName = roleName;
            SchoolId = schoolId;
            Permission = permission;           
        }
        /// <summary>
        /// شناسه نقش مورد نظر
        /// </summary>
        public Guid RoleInSchoolId { get; private set; }


        /// <summary>
        /// عنوان نقش
        /// </summary>
        public string RoleName { get; private set; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        public Guid SchoolId { get; private set; }

        /// <summary>
        /// دسترسی های نقش
        /// </summary>
        public List<IPermissionEvent> Permission { get; private set; }
        
    }
}
