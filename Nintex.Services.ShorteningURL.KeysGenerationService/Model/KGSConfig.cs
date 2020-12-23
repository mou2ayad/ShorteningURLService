using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nintex.Services.ShorteningURL.KeysGenerationService.Model
{
    public class KGSConfig
    {
        public int Capacity { set; get; } = 1000;
        public int LowThreshold { set; get; } = 100;
        public int KeysBatchSize { set; get; } = 50;
        public string DBConnectionString { set; get; }

    }
}
