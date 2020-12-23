
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nintex.NetCore.Component.ShorteningURL.Contracts
{
    public interface IShorteningService
    {
        Task<string> RegisterUrlAsync(string uri);
        string GetActualUrlByShortkey(string shortkey);

    }
}
