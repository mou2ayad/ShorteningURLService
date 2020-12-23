using System.Collections.Generic;

namespace Nintex.Services.ShorteningURL.KeysGenerationService.Service
{
    public interface IKGS
    {
        IEnumerable<string> GenerateNewKeys(int size);
        string GenerateNewKey();
    }
}