using System;
using System.Collections.Generic;
using Holding.Base.Infrastructure.Messaging;

namespace Holding.Base.Event.Role
{
    public interface IRolePermissionHasBeenSetEvent : IMessage
    {
        /// <summary>
        /// شناسه نقش مورد نظر
        /// </summary>
        Guid RoleInSchoolId { get; }

        /// <summary>
        /// عنوان نقش
        /// </summary>
        string RoleName { get; }
        /// <summary>
        /// شناسه مدرسه
        /// </summary>
        Guid SchoolId { get; }

        /// <summary>
        /// دسترسی های نقش
        /// </summary>
        List<IPermissionEvent> Permission { get; }
    }
}
