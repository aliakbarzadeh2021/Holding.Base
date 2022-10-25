using System;
using System.Configuration;
using Holding.Base.Sync.Enums;
using Holding.Base.Sync.Models;

namespace Holding.Base.Sync.Configurations
{
    public class SyncApplicationSettings : ISyncApplicationSettings
    {
        public string SyncDatabaseAddress
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["MongoConnectionString"].ToString();
            }
        }

        public string SyncDatabaseName
        {
            get { return ConfigurationManager.AppSettings["MongoDatabaseName"]; }
        }


    }
}