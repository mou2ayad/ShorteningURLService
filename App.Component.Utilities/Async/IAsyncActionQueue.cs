using System;

namespace App.Component.Utilities.Async
{
    public interface IAsyncActionQueue
    {
        void FireAndForget(Delegate action, params object[] args);
    }
}