using System;

namespace BLL.Model.Managers
{
    public abstract class ManagerBase
    {
        public virtual event EventHandler<BLLEventArgs> ManagerEvent;

        public virtual string ModuleName { get { return this.GetType().Name; } }

        public void InvokeManagerEvent(string message)
        {
            EventHandler<BLLEventArgs> handler = ManagerEvent;
            if (handler != null)
                handler(this,
                        new BLLEventArgs
                            {
                                Module = ModuleName,
                                MessageDescription = message
                            });
        }

        public void InvokeManagerEvent(EventType eventType, string messageKey = null, string messageDescription = null)
        {
            EventHandler<BLLEventArgs> handler = ManagerEvent;
            if (handler != null)
                handler(this,
                        new BLLEventArgs
                            {
                                Module = ModuleName,
                                MessageKey = messageKey,
                                MessageDescription = messageDescription,
                                EventType = eventType
                            });
        }

        public void InvokeManagerEvent(Exception exception)
        {
            EventHandler<BLLEventArgs> handler = ManagerEvent;
            if (handler != null) handler(this, new BLLEventArgs { Module = ModuleName, Exception = exception, EventType = EventType.Exception });
        }

        public void InvokeManagerEvent(BLLEventArgs eventArgs)
        {
            EventHandler<BLLEventArgs> handler = ManagerEvent;
            if (handler != null) handler(this, eventArgs);
        }
    }
}