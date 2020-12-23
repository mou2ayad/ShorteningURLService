using System;

namespace Nintex.NetCore.Component.Utilities.Async
{
    public interface IAsyncActionQueue
    {
        void FireAndForget(Delegate action, params object[] args);
    }
}