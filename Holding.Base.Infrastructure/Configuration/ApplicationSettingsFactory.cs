namespace Holding.Base.Infrastructure.Configuration
{
    public static class ApplicationSettingsFactory
    {
        private static IApplicationSettings applicationSettings;

        public static void InitializeApplicationSettingsFactory(
                                      IApplicationSettings settings)
        {
            applicationSettings = settings;
        }

        public static IApplicationSettings GetApplicationSettings()
        {
            return applicationSettings;
        }
    }
}
