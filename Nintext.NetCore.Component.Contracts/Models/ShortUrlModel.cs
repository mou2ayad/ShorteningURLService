using System;
using System.Collections.Generic;
using System.Text;

namespace Nintex.NetCore.Component.Contracts
{
    public class ShortUrlModel
    {
        public string ShortKey { set; get; }
        public string FullUrl { set; get; }
        public DateTime LastReadingDate { set; get; }
        public DateTime CreationDate { set; get; }

    }
}
