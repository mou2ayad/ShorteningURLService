
using System.Threading.Tasks;

namespace Nintex.Services.ShorteningURL.KeysGenerationService.DataAccess
{
    public interface IKGSDBService
    {
        Task<int> InsertNewRound(Kgsround kgsround);
        Task<Kgsround> GetLatestKGSRound();
    }
}