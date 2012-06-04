using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BLL.Model.Managers;

namespace BLL.FixedAssetSchedule
{
    public class DepreciationRateManager : ManagerBase, IDepreciationRateManager
    {
        public DepreciationRateManager()
        { 
        }

        public bool Set(string projectName, string headName, double rate)
        {
            return true;
        }
    }
}
