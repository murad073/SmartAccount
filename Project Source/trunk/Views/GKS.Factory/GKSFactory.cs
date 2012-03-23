using SQL2K8;
using BLL.Model.Repositories;


namespace GKS.Factory
{
    public static class GKSFactory
    {
        public static IRecordRepository GetRecordRepository()
        {
            return new RecordRepository();
        }

        public static IProjectRepository GetProjectRepository()
        {
            return new ProjectRepository();
        }

        public static IHeadRepository GetHeadRepository()
        {
            return new HeadRepository();
        }

        public static ILedgerRepository GetLedgerRepository()
        {
            return new LedgerRepository();
        }

        public static IParameterRepository GetParameterRepository()
        {
            return new ParameterRepository();
        }
    }
}

