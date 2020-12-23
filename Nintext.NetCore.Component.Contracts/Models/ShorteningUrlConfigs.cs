using System;
using System.Collections.Generic;
using System.Text;

namespace Nintex.NetCore.Component.Common.Models
{
    public class ShorteningUrlConfigs
    {
        public string DefaultSchema { set; get; } = "https";
        public string SqlServerStorageConnectionString { set; get; }
        public bool EnableCaching { set; get; }
        public string CachingType { set; get; }
        public int ValuesInCacheExpireAfter { set; get; }
        public string KGSUrl { set; get; }
        public string BaseUrl { set; get; }
    }
}
