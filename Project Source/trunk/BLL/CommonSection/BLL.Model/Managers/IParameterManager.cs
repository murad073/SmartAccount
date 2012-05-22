using System;

namespace BLL.Model.Managers
{
    public interface IParameterManager
    {
        DateTime GetCurrentFinantialYearStartDate();
        void SetCurrentFinancialYear(string currentFinantialYear);
        event EventHandler<BLLEventArgs> ManagerEvent;
        string ModuleName { get; }
        void InvokeManagerEvent(string message);
        void InvokeManagerEvent(EventType eventType, string messageKey = null, string messageDescription = null);
        void InvokeManagerEvent(Exception exception);
        void InvokeManagerEvent(BLLEventArgs eventArgs);
        string GetCurrentFinantialYear();
    }
}