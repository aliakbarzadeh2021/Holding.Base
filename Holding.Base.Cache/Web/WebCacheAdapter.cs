using System;
using System.Web.Caching;
using Holding.Base.Cache;
using Holding.Base.Cache.Core.Diagnostics;

namespace Holding.Base.Cache.Web
{
    public class WebCacheAdapter : ICache
    {
        private System.Web.Caching.Cache _cache;
    	private ILogging _logger;

        public WebCacheAdapter(ILogging logger)
        {
        	_logger = logger;

			if (System.Web.HttpContext.Current != null)
				_cache = System.Web.HttpContext.Current.Cache;
			else
				_cache = System.Web.HttpRuntime.Cache;
        }

		public void Add(string cacheKey, DateTime expiry, object dataToAdd)
		{
			if (dataToAdd != null)
			{
                _cache.Add(cacheKey, dataToAdd, null, expiry, System.Web.Caching.Cache.NoSlidingExpiration, CacheItemPriority.Normal, null);
				_logger.WriteInfoMessage(string.Format("Adding data to cache with cache key: {0}, expiry date {1}",cacheKey,expiry.ToString("yyyy/MM/dd hh:mm:ss")));
			}
		}

    	public T Get<T>(string cacheKey) where T : class
        {
            T data = _cache.Get(cacheKey) as T;
			if (data == null)
			{
				if (System.Web.HttpContext.Current.Items.Contains(cacheKey))
				{
					_logger.WriteInfoMessage(string.Format("Getting data from per request cache with cache key: {0}", cacheKey));
					return System.Web.HttpContext.Current.Items[cacheKey] as T;
				}
			}
            return data;
        }

        public void InvalidateCacheItem(string cacheKey)
        {
            if (_cache.Get(cacheKey) != null)
                _cache.Remove(cacheKey);
        }

		public void Add(string cacheKey, TimeSpan slidingExpiryWindow, object dataToAdd) 
		{
			if (dataToAdd != null)
			{
				_logger.WriteInfoMessage(string.Format("Adding data to cache with cache key: {0}, sliding window expiry in seconds {1}", cacheKey, slidingExpiryWindow.TotalSeconds));
                _cache.Add(cacheKey, dataToAdd, null, System.Web.Caching.Cache.NoAbsoluteExpiration, slidingExpiryWindow, CacheItemPriority.BelowNormal,
				           null);
			}
		}

		public void AddToPerRequestCache(string cacheKey, object dataToAdd)
		{
			if (dataToAdd != null && System.Web.HttpContext.Current != null)
			{
				_logger.WriteInfoMessage(string.Format("Adding data to per request cache with cache key: {0}", cacheKey));

				if (!System.Web.HttpContext.Current.Items.Contains(cacheKey))
				{
					System.Web.HttpContext.Current.Items.Add(cacheKey,dataToAdd);
				}
				else
				{
					System.Web.HttpContext.Current.Items[cacheKey] = dataToAdd;
				}
				
			}
		}

		public CacheSetting CacheType
		{
			get { return CacheSetting.Web; }
		}
	}
}
