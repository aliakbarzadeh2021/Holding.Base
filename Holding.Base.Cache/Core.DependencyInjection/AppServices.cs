using Holding.Base.Cache;
using Holding.Base.Cache.Web;
using Holding.Base.CacheAdapter;
using Holding.Base.Cache.Core.Diagnostics;
using Holding.Base.Cache.Distributed.AppFabric;
using Holding.Base.Cache.Distributed.MemCached;

namespace Holding.Base.Cache.Core.DependencyInjection
{
    public static class AppServices
    {
    	private static ICacheProvider cacheProvider;
    	private static ICache cache;
    	private static ILogging logger;
    	private static bool isInitialised = false;
		private static readonly object LockRef = new object();

    	static AppServices()
    	{
    		PreStartInitialise();
    	}

		public static void SetLogger(ILogging logger)
		{
			AppServices.logger = logger;
			isInitialised = false;
			PreStartInitialise();
		}

        /// <summary>
        /// Initialise the container with core dependencies. The cache/cache provider should be set to be
        /// singletons if adapting to use with a Dependency Injection mechanism
        /// </summary>
        /// <remarks>Note: In a .Net 4 web app, this method could be invoked using the new PreApplicationStartMethod attribute
        /// as in: <code>[assembly: PreApplicationStartMethod(typeof(MyStaticClass), "PreStartInitialise")]</code>
        /// Also note this section should be replaced with the DependencyInjection container of choice. This
        /// code simply acts as a cheap mechanism for this without requiring a dependency on a 
        /// container that you dont like/use.
        /// </remarks>
        public static void PreStartInitialise()
        {
			if (!isInitialised)
			{
				lock (LockRef)
				{
					if (!isInitialised)
					{
						isInitialised = true;
						if (logger == null)
						{
							logger = new Logger();
						}
						switch (MainConfig.Default.CacheToUse.ToLowerInvariant())
						{
							case CacheTypes.MemoryCache:
								cache = new MemoryCacheAdapter(logger);
								break;
							case CacheTypes.WebCache:
								cache = new WebCacheAdapter(logger);
								break;
							case CacheTypes.AppFabricCache:
								cache = new AppFabricCacheAdapter(logger);
								break;
							case CacheTypes.Memcached:
								cache = new MemcachedAdapter(logger);
								break;
							default:
								cache = new MemoryCacheAdapter(logger);
								break;
						}
						cacheProvider = new CacheProvider(cache, logger);
					}
				}
			}
        }

		public static ICacheProvider Cache { get { return cacheProvider; } }
    }
}
