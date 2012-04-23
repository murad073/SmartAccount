using System;
using SQL2K8;
using BLL.Model.Repositories;
using SQLCompact;


namespace GKS.Factory
{
    public static class GKSFactory
    {
        public static RepositoryType RepositoryType { get; set; }

        public static IRecordRepository GetRecordRepository()
        {
            switch (RepositoryType)
            {
                case RepositoryType.SqlCompact:
                    return new SQLCompact.RecordRepository();
                case RepositoryType.SqlExpress:
                    return new SQL2K8.RecordRepository();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public static IProjectRepository GetProjectRepository()
        {
            switch (RepositoryType)
            {
                case RepositoryType.SqlCompact:
                    return new SQLCompact.ProjectRepository();
                case RepositoryType.SqlExpress:
                    return new SQL2K8.ProjectRepository();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public static IHeadRepository GetHeadRepository()
        {
            switch (RepositoryType)
            {
                case RepositoryType.SqlCompact:
                    return new SQLCompact.HeadRepository();
                case RepositoryType.SqlExpress:
                    return new SQL2K8.HeadRepository();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public static ILedgerRepository GetLedgerRepository()
        {
            switch (RepositoryType)
            {
                case RepositoryType.SqlCompact:
                    return new SQLCompact.LedgerRepository();
                case RepositoryType.SqlExpress:
                    return new SQL2K8.LedgerRepository();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }

        public static IParameterRepository GetParameterRepository()
        {
            switch (RepositoryType)
            {
                case RepositoryType.SqlCompact:
                    return new SQLCompact.ParameterRepository();
                case RepositoryType.SqlExpress:
                    return new SQL2K8.ParameterRepository();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            
        }
    }

    public enum RepositoryType
    {
        SqlCompact,
        SqlExpress
    }
}

