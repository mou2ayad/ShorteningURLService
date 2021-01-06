using App.Component.ShortUrlStorage.SqlServer.Persistence;
using System.Threading.Tasks;

namespace App.Component.ShortUrlStorage.SqlServer.Service
{
    public interface IShorteningUrlSqlServerStorageService
    {
        Task<Url> InsertURLRowInDBAsync(string ShortKey, string FullUrl);
        Task<Url> SelectUrlRowByKeyAsync(string key);
    }
}