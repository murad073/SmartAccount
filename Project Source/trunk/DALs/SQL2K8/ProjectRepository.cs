using System.Collections.Generic;
using System.Linq;
//using BLL.DALInterface;
using BLL.Model;
using DALProject = SQL2K8.Project;
using BLLProject = BLL.Model.Entity.Project;
using BLL.Model.Repositories;
using IProjectRepository = BLL.Model.Repositories.IProjectRepository;


namespace SQL2K8
{
    public class ProjectRepository : IProjectRepository
    {
        private SmartAccountEntities _db;
        public ProjectRepository()
        {
            _db = DBFactory.Instance.DB;
        }

        public bool RemoveHeadFromProject(int projectId, int headId)
        {
            ProjectHead projectHead = _db.ProjectHeads.Where(pc => pc.ProjectID == projectId && pc.HeadID == headId).SingleOrDefault();
            if (projectHead != null)
            {
                _db.DeleteObject(projectHead);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool AddHeadToProject(int projectId, int headId)
        {
            ProjectHead projectHead = _db.ProjectHeads.Where(pc => pc.ProjectID == projectId && pc.HeadID == headId).SingleOrDefault();
            if (projectHead == null)
            {
                ProjectHead newProjectHead = new ProjectHead { ProjectID = projectId, HeadID = headId, IsActive = true };
                _db.AddToProjectHeads(newProjectHead);
                _db.SaveChanges();
                return true;
            }
            return false;
        }

        public BLLProject Insert(BLLProject entity)
        {
            DALProject dalProject = GetDALProject(entity);
            _db.AddToProjects(dalProject);
            _db.SaveChanges();
            entity.Id = dalProject.ID;
            return entity;
        }

        public BLLProject Delete(int id)
        {
            DALProject dalProject = GetDALProject(id);
            if (dalProject == null) return null;
            _db.DeleteObject(dalProject);
            _db.SaveChanges();
            return GetBLLProject(dalProject);
        }

        public bool Update(BLLProject entity)
        {
            DALProject dalProject = GetDALProject(entity.Name);
            if (dalProject == null) return false;
            dalProject.Description = entity.Description;
            dalProject.IsActive = entity.IsActive;
            dalProject.CreateDate = entity.CreateDate;
            return _db.SaveChanges() > 0;
        }

        public BLLProject Get(int id)
        {
            DALProject dalProject = GetDALProject(id);
            if (dalProject == null) return null;
            return GetBLLProject(dalProject);
        }

        public IList<BLLProject> GetAll()
        {
            return _db.Projects.ToList().Select(GetBLLProject).ToList();
        }

        internal static DALProject GetDALProject(int id)
        {
            return DBFactory.Instance.DB.Projects.Where(p => p.ID == id).SingleOrDefault();
        }

        internal static DALProject GetDALProject(string name)
        {
            return DBFactory.Instance.DB.Projects.Where(p => p.Name == name).SingleOrDefault();
        }

        internal static DALProject GetDALProject(BLLProject bllProject)
        {
            return new DALProject
            {
                ID = bllProject.Id,
                Name = bllProject.Name,
                Description = bllProject.Description,
                IsActive = bllProject.IsActive,
                CreateDate = bllProject.CreateDate
            };
        }

        internal static BLLProject GetBLLProject(DALProject dalProject)
        {
            return new BLLProject
            {
                Id = dalProject.ID,
                Name = dalProject.Name,
                Description = dalProject.Description,
                IsActive = dalProject.IsActive,
                CreateDate = dalProject.CreateDate
            };
        }

        public BLLProject Get(string projectName)
        {
            DALProject dalProject = _db.Projects.Where(p => p.Name == projectName).SingleOrDefault();
            if (dalProject == null) return null;
            return GetBLLProject(dalProject);
        }


        public int GetProjectHeadId(string projectName, string headName)
        {
            return
                _db.ProjectHeads.Where(ph => ph.Project.Name == projectName && ph.Head.Name == headName).SingleOrDefault
                    ().ID;
        }
    }
}

