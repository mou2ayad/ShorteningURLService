using System;
using System.Collections.Generic;

#nullable disable

namespace Nintex.Services.ShorteningURL.KeysGenerationService.DataAccess
{
    public partial class Kgsround
    {
        public decimal RoundId { get; set; }
        public string FromKey { get; set; }
        public string ToKey { get; set; }
        public string LastCounter { get; set; }
        public DateTime RoundDate { get; set; }
    }
}
