using BLL.Model.Entity;
using System.Collections.Generic;
namespace BLL.Model.Managers
{
    public interface IDepreciationRateManager
    {
        IList<DepreciationRate> GetDepreciationRates();
        bool Set(string projectName, string headName, double rate);
    }
}
