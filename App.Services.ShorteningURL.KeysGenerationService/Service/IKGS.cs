using System.Collections.Generic;

namespace App.Services.ShorteningURL.KeysGenerationService.Service
{
    public interface IKGS
    {
        IEnumerable<string> GenerateNewKeys(int size);
        string GenerateNewKey();
    }
}