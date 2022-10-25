using System;
using Holding.Base.Cache;
using Holding.Base.Cache.Core.Diagnostics;

namespace Holding.Base.Cache.Distributed.MemCached
{
	public class MemcachedCacheFactory : DistributedCacheFactoryBase
	{
		private const string DefaultIpAddress = "127.0.0.1";
		private const int DefaultPort = 11211;

		private readonly CacheConfig _config;

		private int _minPoolSize = 10;
		private int _maxPoolSize = 20;
		private TimeSpan _connectTimeout = TimeSpan.FromSeconds(5);
		private TimeSpan _deadNodeTimeout = TimeSpan.FromSeconds(30);


		public MemcachedCacheFactory(ILogging logger) : base(logger)
		{
			_config = ParseConfig(DefaultIpAddress, DefaultPort);
			ExtractCacheSpecificConfig();
		}

		public int MinimumPoolSize { get { return _minPoolSize; }}
		public int MaximumPoolSize { get { return _maxPoolSize; } }
		public TimeSpan ConnectTimeout { get { return _connectTimeout; } }
		public TimeSpan DeadNodeTimeout { get { return _deadNodeTimeout; } }

		public CacheServerFarm ConstructCacheFarm()
		{
			try
			{
				var serverFarm = new CacheServerFarm(Logger);
				serverFarm.Initialise(_config.ServerNodes);
				return serverFarm;
			}
			catch (Exception ex)
			{
				Logger.WriteException(ex);
				throw;
			}
		}

		private void ExtractCacheSpecificConfig()
		{
			var minPoolSizeValue = SafeGetCacheConfigValue(MemcachedConstants.ConfigMinimumConnectionPoolSize);
			var maxPoolSizeValue = SafeGetCacheConfigValue(MemcachedConstants.ConfigMaximumConnectionPoolSize);
			int value;
			if (int.TryParse(minPoolSizeValue, out value))
			{
				_minPoolSize = value;
			}
			if (int.TryParse(maxPoolSizeValue, out value))
			{
				_maxPoolSize = value;
			}

			var connectTimeoutValue = SafeGetCacheConfigValue(MemcachedConstants.ConfigConnectionTimeout);
			var deadTimeoutValue = SafeGetCacheConfigValue(MemcachedConstants.ConfigDeadNodeTimeout);

		    try
			{
				if (connectTimeoutValue != null)
				{
					_connectTimeout = TimeSpan.Parse(connectTimeoutValue);
				}
				if (deadTimeoutValue != null)
				{
					_deadNodeTimeout = TimeSpan.Parse(deadTimeoutValue);
				}
			}
			catch (Exception ex)
			{
				Logger.WriteErrorMessage(string.Format("Unable to parse timeout values. [{0}]", ex.Message));
			}
		}

		private string SafeGetCacheConfigValue(string key)
		{
			if (_config.ProviderSpecificValues.ContainsKey(key))
			{
				return _config.ProviderSpecificValues[key];
			}

			return null;
		}
	}
}
