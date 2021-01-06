using Microsoft.Extensions.Logging;
using App.Component.Utilities.Logger;
using System;
using System.Threading;

namespace App.Component.Utilities.Async
{
    internal class ActionInfo
    {
        public readonly Delegate action;
        public readonly object[] args;
        internal ActionInfo(Delegate action, params object[] args)
        {
            this.action = action;
            this.args = args;
        }
    }

    public class AsyncActionQueue : IAsyncActionQueue
    {
        private readonly ILogger<AsyncActionQueue> _logger;
        public AsyncActionQueue(ILogger<AsyncActionQueue> logger)
        {
            _logger = logger;
            DynamicInvokeActionCall = new WaitCallback(DynamicInvokeAction);
        }
       
        private WaitCallback DynamicInvokeActionCall;

        public void FireAndForget(Delegate action, params object[] args)
        {
            ThreadPool.QueueUserWorkItem(DynamicInvokeActionCall, new ActionInfo(action, args));
        }
       
        private void DynamicInvokeAction(object ob)
        {
            try
            {
                ActionInfo ti = (ActionInfo)ob;
                ti.action.DynamicInvoke(ti.args);
            }
            catch (Exception ex)
            {
                _logger.LogErrorDetails(ex, ob);
            }
        }
    }
}
