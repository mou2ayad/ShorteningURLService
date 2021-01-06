using System;
using System.Collections.Generic;

#nullable disable

namespace App.Component.ShortUrlStorage.SqlServer.Persistence
{
    public partial class Url
    {
        public decimal KeyId { get; set; }
        public string ShortKey { get; set; }
        public string FullUrl { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? LastReadingDate { get; set; }
    }
}
