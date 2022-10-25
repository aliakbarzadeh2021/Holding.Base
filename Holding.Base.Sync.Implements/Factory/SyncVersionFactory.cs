using System;
using Holding.Base.Sync.Models;

namespace Holding.Base.Sync.Implements.Factory
{
    internal static class SyncVersionFactory
    {
        internal static SyncVersion Create()
        {
            var version = Guid.NewGuid().ToString();
            SyncVersion syncVersion=new SyncVersion(version,DateTime.Now);
            return syncVersion;
        }
    }
}