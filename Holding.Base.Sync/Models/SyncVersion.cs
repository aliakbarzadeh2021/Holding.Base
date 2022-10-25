using System;
using System.Collections.Generic;

namespace Holding.Base.Sync.Models
{
    public class SyncVersion : IEqualityComparer<SyncVersion>
    {
        public string Version { get; set; }

        public DateTime DateTime { get; set; }


        public SyncVersion()
        {
        }


        public SyncVersion(string version, DateTime dateTime)
        {
            Version = version;
            DateTime = dateTime;
        }

        public bool Equals(SyncVersion x, SyncVersion y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return string.Equals(x.Version, y.Version);
        }

        public int GetHashCode(SyncVersion obj)
        {
            return (obj.Version != null ? obj.Version.GetHashCode() : 0);
        }
    }
}