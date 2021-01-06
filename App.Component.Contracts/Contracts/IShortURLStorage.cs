using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Component.Common.Contracts
{
    public interface IShortURLStorage
    {
        Task<string> GetUrlByKeyAsync(string key);
        Task<string> CreateNewURLAsync(string ShortKey, string FullUrl);
    }
}
