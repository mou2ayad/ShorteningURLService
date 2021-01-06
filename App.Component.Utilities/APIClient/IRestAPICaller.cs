using System.Threading.Tasks;

namespace App.Component.Utilities.APIClient
{
    public interface IRestAPICaller
    {
        Task<T> CallAPIAsync<T>(string path, string ServiceName);
        Task<string> CallAPIAsStringAsync(string path, string ServiceName);
    }
}