using System.Web.Security;
using Microsoft.Owin.Security.DataProtection;

namespace Holding.Base.Security.Authorization.Protectors
{
    public class MachineKeyDataProtector : IDataProtector
    {
        private readonly string[] purposes;

        public MachineKeyDataProtector(params string[] purposes)
        {
            this.purposes = purposes;
        }

        public byte[] Protect(byte[] userData)
        {
            return MachineKey.Protect(userData, this.purposes);
        }

        public byte[] Unprotect(byte[] protectedData)
        {
            return MachineKey.Unprotect(protectedData, this.purposes);
        }
    }

}