using System.Collections.Generic;

namespace Nintex.Services.ShorteningURL.KeysGenerationService.Service
{
    public interface IKeysQueue
    {
        List<string> Dequeue();
        bool Enqueue(string newkeys);
    }
}