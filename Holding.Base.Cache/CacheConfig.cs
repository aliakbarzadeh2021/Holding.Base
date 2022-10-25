using System.Collections.Generic;
using Holding.Base.CacheAdapter;
using Holding.Base.Cache.Distributed;

namespace Holding.Base.Cache
{
	public class CacheConfig
	{
		private List<ServerNode> _serverNodes = new List<ServerNode>();
		public List<ServerNode> ServerNodes { get { return _serverNodes; } }

		private Dictionary<string, string> _providerSpecificValues = new Dictionary<string, string>();
		public Dictionary<string,string> ProviderSpecificValues { get { return _providerSpecificValues; } }

		private bool _isCacheEnabled = true;
		public bool IsCacheEnabled { get { return _isCacheEnabled; } set { _isCacheEnabled = value; } }

		public CacheConfig()
		{
			IsCacheEnabled = MainConfig.Default.IsCacheEnabled;
		}
	}
}
