﻿using System.Collections.Generic;

namespace App.Services.ShorteningURL.KeysGenerationService.Service
{
    public interface IKeysQueue
    {
        List<string> Dequeue();
        bool Enqueue(string newkeys);
    }
}