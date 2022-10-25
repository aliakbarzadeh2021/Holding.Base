namespace Holding.Base.Cache.Distributed.AppFabric
{
	public static class AppFabricConstants
	{
		public const string DefaultServerAddress = "localhost";
		public const string ConfigCacheNameKey = "DistributedCacheName";
		public const string ConfigSecurityModeKey = "SecurityMode";
		public const string ConfigSecurityMessageAuthorisationKey = "MessageSecurityAuthorizationInfo";
		public const string ConfigUseSslKey = "UseSsl";

		public const string ConfigSecurityModeMessage = "message";
		public const string ConfigSecurityModeNone = "none";
		public const string ConfigSecurityModeTransport = "transport";

		public const int DefaultPort = 22233;
	}
}
