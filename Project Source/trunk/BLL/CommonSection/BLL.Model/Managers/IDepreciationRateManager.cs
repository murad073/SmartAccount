using BLL.Model.Entity;
using System.Collections.Generic;
namespace BLL.Model.Managers
{
    public interface IDepreciationRateManager
    {
        IList<DepreciationRate> GetDepreciationRates();
        bool Set(Project project, Head head, double rate);
    }
}
