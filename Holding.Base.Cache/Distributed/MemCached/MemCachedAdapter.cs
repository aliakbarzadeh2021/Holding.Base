using System;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Holding.Base.Cache;
using Holding.Base.Cache.Core.Diagnostics;

namespace Holding.Base.Cache.Distributed.MemCached
{
	public class MemcachedAdapter : ICache
	{
		private CacheServerFarm _serverFarm;
		private ILogging _logger;

		private static Enyim.Caching.IMemcachedClient _client;
		private static object _lockRef = new object();
		private static bool _isInitialised = false;


		public MemcachedAdapter(ILogging logger)
		{
			_logger = logger;

			var factory = new MemcachedCacheFactory(_logger);
			_serverFarm = factory.ConstructCacheFarm();

			if (_serverFarm == null || _serverFarm.NodeList == null || _serverFarm.NodeList.Count == 0)
				throw new ArgumentException("Must specify at least 1 server node to use for memcached");

			Initialise(factory);
			LogManager.AssignFactory(new LogFactoryAdapter(_logger));
		}

		private void Initialise(MemcachedCacheFactory factory)
		{
			if (!_isInitialised)
			{
				lock (_lockRef)
				{
					if (!_isInitialised)
					{
						_isInitialised = true;
						// If the consumer of this class has passed in a IMemcachedClient
						//instance, then we use that instead
						if (_client != null)
						{
							return;
						}
						var config = new Enyim.Caching.Configuration.MemcachedClientConfiguration();
						_serverFarm.NodeList.ForEach(n => config.AddServer(n.IPAddressOrHostName,n.Port));
						config.SocketPool.ConnectionTimeout = factory.ConnectTimeout;
						config.SocketPool.DeadTimeout = factory.DeadNodeTimeout;
						config.SocketPool.MinPoolSize = factory.MinimumPoolSize;
						config.SocketPool.MaxPoolSize = factory.MaximumPoolSize;
						config.Protocol = MemcachedProtocol.Text;
						config.Transcoder = new DataContractTranscoder();
						_client = new MemcachedClient(config);
						_logger.WriteInfoMessage("memcachedAdapter initialised.");
					}
				}
			}
		}


		public T Get<T>(string cacheKey) where T : class
		{
			try
			{
				var sanitisedKey = SanitiseCacheKey(cacheKey);
				// try per request cache first, but only if in a web context
				if (InWebContext())
				{
					if (System.Web.HttpContext.Current.Items.Contains(cacheKey))
					{
						var data = System.Web.HttpContext.Current.Items[cacheKey];
						var realData = data as T;
						if (realData != null)
						{
							return realData;
						}
					}
				}
				return _client.Get<T>(sanitisedKey);
			}
			catch (Exception ex)
			{
				_logger.WriteException(ex);
			}
			return null;
		}

		public void Add(string cacheKey, DateTime absoluteExpiry, object dataToAdd)
		{
			try
			{
				var sanitisedKey = SanitiseCacheKey(cacheKey);
				var success = _client.Store(StoreMode.Set, sanitisedKey, dataToAdd, absoluteExpiry);
				if (!success)
				{
					_logger.WriteErrorMessage(string.Format("Unable to store item in cache. CacheKey:{0}",sanitisedKey));
				}
			}
			catch (Exception ex)
			{
				_logger.WriteException(ex);
			}
		}

		public void Add(string cacheKey, TimeSpan slidingExpiryWindow, object dataToAdd)
		{
			try
			{
				var sanitisedKey = SanitiseCacheKey(cacheKey);
				var success = _client.Store(StoreMode.Set, sanitisedKey, dataToAdd, slidingExpiryWindow);
				if (!success)
				{
					_logger.WriteErrorMessage(string.Format("Unable to store item in cache. CacheKey:{0}", sanitisedKey));
				}
			}
			catch (Exception ex)
			{
				_logger.WriteException(ex);
			}
		}

		public void InvalidateCacheItem(string cacheKey)
		{
			try
			{
				var sanitisedKey = SanitiseCacheKey(cacheKey);
				var success = _client.Remove(sanitisedKey);
				if (!success)
				{
					_logger.WriteErrorMessage(string.Format("Unable to remove item from cache. CacheKey:{0}",sanitisedKey));
				}
			}
			catch (Exception ex)
			{
				_logger.WriteException(ex);
			}
		}

		public void AddToPerRequestCache(string cacheKey, object dataToAdd)
		{
			// If not in a web context, do nothing
			if (InWebContext())
			{
				if (System.Web.HttpContext.Current.Items.Contains(cacheKey))
				{
					System.Web.HttpContext.Current.Items.Remove(cacheKey);
				}
				System.Web.HttpContext.Current.Items.Add(cacheKey,dataToAdd);
			}
		}

		private bool InWebContext()
		{
			return (System.Web.HttpContext.Current != null);
		}

		public CacheSetting CacheType
		{
			get { return CacheSetting.MemCached; }
		}

		private string SanitiseCacheKey(string cacheKey)
		{
			if (string.IsNullOrWhiteSpace(cacheKey))
			{
				throw new ArgumentException("Cannot have an empty or NULL cache key");
			}
			return cacheKey.Replace(" ", string.Empty).Replace("#","-");
		}
	}
}
