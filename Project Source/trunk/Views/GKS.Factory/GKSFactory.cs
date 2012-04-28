using System;
using BLL.Model.Entity;
using BLL.Model.Repositories;
using CodeFirst;


namespace GKS.Factory
{
    public static class GKSFactory
    {
        public static RepositoryType RepositoryType { get; set; }

        public static IRepository<T> GetRepository<T>() where T : class, new()
        {
            switch (RepositoryType)
            {
                case RepositoryType.CodeFirst:
                    return new Repository<T>();
                //case RepositoryType.Mock:
                //    return new SQL2K8.RecordRepository();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        //public static IRepository<Project> GetProjectRepository()
        //{
        //    switch (RepositoryType)
        //    {
        //        case RepositoryType.CodeFirst:
        //            return new SQLCompact.ProjectRepository();
        //        case RepositoryType.Mock:
        //            return new SQL2K8.ProjectRepository();
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        //public static IRepository<Head> GetHeadRepository()
        //{
        //    switch (RepositoryType)
        //    {
        //        case RepositoryType.CodeFirst:
        //            return new SQLCompact.HeadRepository();
        //        //case RepositoryType.Mock:
        //        //    return new SQL2K8.HeadRepository();
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        //public static IRepository<Record> GetLedgerRepository()
        //{
        //    switch (RepositoryType)
        //    {
        //        case RepositoryType.CodeFirst:
        //            return new SQLCompact.LedgerRepository();
        //        case RepositoryType.Mock:
        //            return new SQL2K8.LedgerRepository();
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}

        //public static IParameterRepository GetParameterRepository()
        //{
        //    switch (RepositoryType)
        //    {
        //        case RepositoryType.CodeFirst:
        //            return new SQLCompact.ParameterRepository();
        //        case RepositoryType.Mock:
        //            return new SQL2K8.ParameterRepository();
        //        default:
        //            throw new ArgumentOutOfRangeException();
        //    }
        //}
    }

    public enum RepositoryType
    {
        CodeFirst,
        Mock
    }
}

