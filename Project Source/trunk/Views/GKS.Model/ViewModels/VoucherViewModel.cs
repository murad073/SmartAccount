using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

using BLL.LedgerManagement;
using BLL.Messaging;
using BLL.Model.Schema;
using BLL.ProjectManagement;
using GKS.Factory;
using BLL.Model.Repositories;

namespace GKS.Model.ViewModels
{
    public class VoucherViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public VoucherViewModel()
        { 
        }

        public IList<Project> _allProjects;
        public IList<Project> AllProjects
        {
            get
            {
                return _allProjects;
            }
            set
            {
                _allProjects = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("AllProjects"));
            }
        }

        private Project _selectedProject;
        public Project SelectedProject
        {
            get
            {
                return _selectedProject;
            }
            set
            {
                _selectedProject = value;

                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AllHeads"));
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedHead"));
                }
            }
        }
    }
}
