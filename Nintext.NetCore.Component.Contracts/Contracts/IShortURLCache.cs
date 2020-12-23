using System;
using System.Collections.Generic;
using System.Text;

namespace Nintex.NetCore.Component.Common.Contracts
{
    public interface IShortURLCache
    {
        bool TryGetURL(string key,out string FullUrl);
        string CacheNewUrl(string key, string FullUrl);
    }
}
