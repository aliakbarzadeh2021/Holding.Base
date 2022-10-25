using System;
using System.Collections.Generic;
using System.Security;
using Holding.Base.Cache;
using Holding.Base.CacheAdapter;
using Holding.Base.Cache.Core.Diagnostics;
using Microsoft.ApplicationServer.Caching;

namespace Holding.Base.Cache.Distributed.AppFabric
{
    public class AppFabricCacheFactory: DistributedCacheFactoryBase
    {
        public AppFabricCacheFactory(ILogging logger) : base(logger)
        {
        }

        public DataCache ConstructCache()
        {
			var config = ParseConfig(AppFabricConstants.DefaultServerAddress, AppFabricConstants.DefaultPort);
			var dataCacheEndpoints = new List<DataCacheServerEndpoint>();
			config.ServerNodes.ForEach(e => dataCacheEndpoints.Add(new DataCacheServerEndpoint(e.IPAddressOrHostName,e.Port)));

            var factoryConfig = new DataCacheFactoryConfiguration();
			factoryConfig.Servers = dataCacheEndpoints;
			SetSecuritySettings(config, factoryConfig);

            try
            {
				Logger.WriteInfoMessage("Constructing AppFabric Cache");

				var factory = new DataCacheFactory(factoryConfig);
                DataCacheClientLogManager.ChangeLogLevel(System.Diagnostics.TraceLevel.Error);

				// Note: When setting up AppFabric. The configured cache needs to be created by the admin using the New-Cache powershell command
            	string cacheName;
				// Prefer the new config mechanismover the explicit entry but still support it. So we
				// try and extract config from the ProviderSpecificValues first.
				if (config.ProviderSpecificValues.ContainsKey(AppFabricConstants.ConfigCacheNameKey))
				{
					cacheName = config.ProviderSpecificValues[AppFabricConstants.ConfigCacheNameKey];
				} else
				{
					cacheName = MainConfig.Default.DistributedCacheName;
				}

				Logger.WriteInfoMessage(string.Format("Appfabric Cache Name: [{0}]", cacheName));

            	DataCache cache = null;
				if (string.IsNullOrWhiteSpace(cacheName))
				{
					cache = factory.GetDefaultCache();
				}
				else
				{
					cache = factory.GetCache(cacheName);
				}

				Logger.WriteInfoMessage("AppFabric cache constructed.");

                return cache;
            }
            catch (Exception ex)
            {
                Logger.WriteException(ex);
                throw;
            }
        }

		private void SetSecuritySettings(CacheConfig config, DataCacheFactoryConfiguration factoryConfig)
		{
			string securityModeValue = null;
			// Set the security mode if required
			if (config.ProviderSpecificValues.ContainsKey(AppFabricConstants.ConfigSecurityModeKey))
			{
				securityModeValue = config.ProviderSpecificValues[AppFabricConstants.ConfigSecurityModeKey];
				Logger.WriteInfoMessage(string.Format("Setting AppFabric security mode:[{0}]", securityModeValue));
			}

			// Set the authorization info/value if required
			string securityAuthValue = null;
			if (config.ProviderSpecificValues.ContainsKey(AppFabricConstants.ConfigSecurityMessageAuthorisationKey))
			{
				securityAuthValue = config.ProviderSpecificValues[AppFabricConstants.ConfigSecurityMessageAuthorisationKey];
				Logger.WriteInfoMessage("Setting AppFabric security Authorisation value");
			}

			string useSslValue = null;
			if (config.ProviderSpecificValues.ContainsKey(AppFabricConstants.ConfigUseSslKey))
			{
				useSslValue = config.ProviderSpecificValues[AppFabricConstants.ConfigUseSslKey];
				Logger.WriteInfoMessage(string.Format("AppFabric Use Ssl: [{0}]",useSslValue));
			}

			var normalisedSecurityMode = string.IsNullOrWhiteSpace(securityModeValue) ? string.Empty : securityModeValue.ToLowerInvariant();
			var normalisedSslValue = string.IsNullOrWhiteSpace(useSslValue) ? string.Empty : useSslValue.ToLowerInvariant();
			if (!string.IsNullOrWhiteSpace(securityAuthValue))
			{
				if (normalisedSecurityMode == AppFabricConstants.ConfigSecurityModeMessage)
				{
					var secureToken = new SecureString();
					foreach (var ch in securityAuthValue)
					{
						secureToken.AppendChar(ch);
					}
					bool useSsl = false;
					if (normalisedSslValue == CacheConstants.ConfigValueTrueText || normalisedSslValue == CacheConstants.ConfigValueTrueNumeric)
					{
						useSsl = true;
					}
					DataCacheSecurity securityProps = new DataCacheSecurity(secureToken, useSsl);
					factoryConfig.SecurityProperties = securityProps;
				}

				if (normalisedSecurityMode == AppFabricConstants.ConfigSecurityModeTransport)
				{
					DataCacheSecurity securityProps = new DataCacheSecurity(DataCacheSecurityMode.Transport,DataCacheProtectionLevel.None);
					factoryConfig.SecurityProperties = securityProps;
				}
			}
		}

    }
}
