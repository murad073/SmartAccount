using System.Collections.Generic;
using BLL.Model.Entity;

namespace BLL.Model.Managers
{
    public interface IRecordManager
    {
        void SetRecords(IList<Record> records);
        bool Save();
    }
}