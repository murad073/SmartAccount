using System.Collections.Generic;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface IHeadManager
    {
        IList<Head> GetHeads(bool isCashOrBankIncluded = true, bool bringInactive = true);
        IList<Head> GetHeads(Project project, bool isCashOrBankIncluded = true, bool bringInactive = true);
        IList<Head> GetCapitalHeads(Project project, bool isCashOrBankIncluded = true, bool bringInactive = true);
        bool Add(Head head);
        bool Update(Head head);
    }
}