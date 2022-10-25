namespace Holding.Base.Sync.Configurations
{
    public static class SyncApplicationSettingsFactory
    {
        private static ISyncApplicationSettings _applicationSettings;

        public static void InitializeApplicationSettingsFactory(
            ISyncApplicationSettings settings)
        {
            _applicationSettings = settings;
        }

        public static ISyncApplicationSettings GetApplicationSettings()
        {
            return _applicationSettings;
        }

        public static SyncApplicationSettings GetSolidApplicationSettings()
        {
            return new SyncApplicationSettings();
        }
    }
}