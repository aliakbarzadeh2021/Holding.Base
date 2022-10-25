using System.Collections.Generic;

namespace Holding.Base.QueryModel.Shared
{
    public class SystemBehavior
    {
        public uint Order { get; set; }
        public string Title { get; set; }
        public string Action { get; set; }
        public string Section { get; set; }
        public string Behavior { get; set; }
        public List<SystemBehavior> Behaviors { get; set; }
    }
}
