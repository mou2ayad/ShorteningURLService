﻿using Microsoft.Extensions.Options;
using App.Component.Utilities.Async;
using App.Services.ShorteningURL.KeysGenerationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Services.ShorteningURL.KeysGenerationService.Service
{
    public class KeysQueue : IKeysQueue
    {
        private Queue<string> keys;
        private readonly KGSConfig _config;
        private readonly IKGS _kGS;
        private readonly IAsyncActionQueue _asyncActionQueue;
        private readonly object lock1 = new object();
        private delegate void startEnqueueDelegate();
        private readonly startEnqueueDelegate _startEnqueue;
        public KeysQueue(IOptions<KGSConfig> options, IKGS kGS, IAsyncActionQueue asyncActionQueue)
        {
            _config = options.Value;
            _kGS = kGS;
            _asyncActionQueue = asyncActionQueue;
            keys = new Queue<string>(_config.Capacity);
            EnqueueBatch(_config.Capacity);
            _startEnqueue = delegate() {
                if(_config.LowThreshold>= keys.Count)
                    EnqueueBatch(_config.Capacity- keys.Count);
            };
        }
        private void EnqueueBatch(int size)
        {
            foreach (var key in _kGS.GenerateNewKeys(size))
                Enqueue(key);
        }
        public bool Enqueue(string newkeys)
        {
            if (keys.Count == _config.Capacity)
                return false;
            keys.Enqueue(newkeys);
            return true;
        }
        public List<string> Dequeue()
        {
            lock (lock1)
            {
                List<string> BatchKeys = new List<string>(_config.KeysBatchSize);
                for (int i = 0; i < _config.KeysBatchSize; i++)
                    BatchKeys.Add(keys.Dequeue());
                _asyncActionQueue.FireAndForget(_startEnqueue);
                return BatchKeys;
            }
        }

    }
}
