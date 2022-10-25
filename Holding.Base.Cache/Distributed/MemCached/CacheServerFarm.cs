using System.Collections.Generic;
using Holding.Base.Cache.Core.Diagnostics;

namespace Holding.Base.Cache.Distributed.MemCached
{
	public class CacheServerFarm
	{
		private List<ServerNode> _nodes;
		private ILogging _logger;

		public List<ServerNode> NodeList { get { return _nodes; } }

		public CacheServerFarm(ILogging logger)
		{
			_logger = logger;
		}

		public void Initialise(List<ServerNode> nodes)
		{
			_nodes = nodes;
		}
	}

}
