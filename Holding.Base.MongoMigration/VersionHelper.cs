using System;

namespace Holding.Base.MongoMigration
{
    public class VersionHelper
    {

        public static Version MaxValue()
        {
            return new Version(int.MaxValue, int.MaxValue, int.MaxValue, int.MaxValue);
        }

        public static Version MinValue()
        {
            return new Version(0, 0, 0, 0);
        }
    }
}
