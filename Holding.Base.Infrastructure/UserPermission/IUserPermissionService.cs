using System.Collections.Generic;

namespace Holding.Base.Infrastructure.UserPermission
{
    public interface IUserPermissionService
    {
        bool HasPermission(IDictionary<string, string> claimsDictionary, string controller, string action);
    }
}