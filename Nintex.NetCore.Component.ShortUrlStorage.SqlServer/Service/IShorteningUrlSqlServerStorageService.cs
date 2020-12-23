using Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Persistence;
using System.Threading.Tasks;

namespace Nintex.NetCore.Component.ShortUrlStorage.SqlServer.Service
{
    public interface IShorteningUrlSqlServerStorageService
    {
        Task<Url> InsertURLRowInDBAsync(string ShortKey, string FullUrl);
        Task<Url> SelectUrlRowByKeyAsync(string key);
    }
}