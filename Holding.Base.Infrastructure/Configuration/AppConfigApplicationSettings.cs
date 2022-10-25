using System;
using Holding.Base.Infrastructure.Enums;
using System.Configuration;

namespace Holding.Base.Infrastructure.Configuration
{
    public class AppConfigApplicationSettings : IApplicationSettings
    {
        public string SqlConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["SqlConnectionString"].ToString();
            }
        }

        public string FileEventSqlConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["FileEventSqlConnectionString"].ToString();
            }
        }

        public string SubScriptionDbConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["SubscriptionDbConnectionString"].ToString(); }
        }
        public string MongoConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MongoConnectionString"].ToString();
            }
        }
        public string MongoDatabaseName
        {
            get
            {
                return ConfigurationManager.AppSettings["MongoDatabaseName"];
            }
        }
        public Language CurrentLanguage
        {
            get { return Language.Persian; }
        }

        public bool IsReadOnlyDataBase
        {
            get
            {
                return ConfigurationManager.AppSettings["IsReadOnlyDataBase"].Equals("True") ? true : false;
            }
        }
        public bool IsSyncActive
        {
            get
            {
                try
                {
                    return ConfigurationManager.AppSettings["IsSyncActive"].Equals("True") ? true : false;
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

    }
}
