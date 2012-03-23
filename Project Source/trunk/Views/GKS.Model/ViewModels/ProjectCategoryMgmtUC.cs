using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Navigation;
using BLL;
using BLL.CommonModel.ProjectCategory;
using GKS.Factory;

namespace GKS.Model.ViewModels
{
    public class ProjectCategoryModel : INotifyPropertyChanged
    {
        ProjectManager _projectManager;
        CategoryManager _categoryManager;

        IList<BLLCategory> _allCategories;

        public ProjectCategoryModel()
        {
            _projectManager = new ProjectManager(GKSFactory.GetProjectManager());
            _categoryManager = new CategoryManager(GKSFactory.GetCategoryManager());

            _allCategories = _categoryManager.GetCategories().ToList();

            AddCategoryButtonClicked = new AddCategoryInProjet(this);
            RemoveCategoryButtonClicked = new RemoveCategoryFromProjet(this);
        }

        // project list box - left sided
        public IList<BLLProject> AllProjectItems
        {
            get
            {
                return _projectManager.GetProjects().ToList();
            }
        }

        private BLLProject _projectSelected;
        public BLLProject ProjectSelected
        {
            get
            {
                return _projectSelected;
            }
            set
            {
                _projectSelected = value;

                CategoriesForProject = _categoryManager.GetCategories(value.Id).ToList().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToArray();
                int[] ids = _categoriesForProject.Select(c => c.Key).ToArray();
                RemainingCategories = _allCategories.Where(c => !ids.Contains(c.Id)).ToList().Select(p => new KeyValuePair<int, string>(p.Id, p.Name)).ToArray();
                SelectedRemainingCategory = SelectedCategoryForProject = new KeyValuePair<int, string>(0, null);
            }
        }

        // remaining Categories list box - right sided
        private KeyValuePair<int, string>[] _remainingCategories;
        public KeyValuePair<int, string>[] RemainingCategories
        {
            get
            {
                return _remainingCategories;
            }
            set
            {
                _remainingCategories = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("RemainingCategories"));
            }

        }

        private KeyValuePair<int, string> _selectedRemainingCategory;
        public KeyValuePair<int, string> SelectedRemainingCategory
        {
            get
            {
                return _selectedRemainingCategory;
            }

            set
            {
                _selectedRemainingCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedRemainingCategory"));
                }
            }
        }

        // Project-Category list box - middle placed
        private KeyValuePair<int, string>[] _categoriesForProject;
        public KeyValuePair<int, string>[] CategoriesForProject 
        {
            get
            {
                return _categoriesForProject;
            }
            set
            {
                _categoriesForProject = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CategoriesForProject"));
            }
        }

        private KeyValuePair<int, string> _SelectedCategoryForProject;
        public KeyValuePair<int, string> SelectedCategoryForProject
        {
            get
            {
                return _SelectedCategoryForProject;
            }
            set
            {
                _SelectedCategoryForProject = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCategoryForProject"));
                }
            }
        }

        public ICommand AddCategoryButtonClicked { get; set; }
        public ICommand RemoveCategoryButtonClicked { get; set; }


        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class AddCategoryInProjet : ICommand
    {
        ProjectCategoryModel _projectCategoryModel;
        public AddCategoryInProjet(ProjectCategoryModel projectCategoryModel)
        {
            _projectCategoryModel = projectCategoryModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            KeyValuePair<int, string> category = _projectCategoryModel.SelectedRemainingCategory;

            if (category.Key > 0)
            {

                List<KeyValuePair<int, string>> existingCategories = _projectCategoryModel.CategoriesForProject.ToList();
                existingCategories.Add(category);
                _projectCategoryModel.CategoriesForProject = existingCategories.ToArray();

                KeyValuePair<int, string> removableCategory = _projectCategoryModel.RemainingCategories.Where(rc => rc.Key == category.Key).SingleOrDefault();
                List<KeyValuePair<int, string>> existingRemovableCategories = _projectCategoryModel.RemainingCategories.ToList();
                existingRemovableCategories.Remove(removableCategory);
                _projectCategoryModel.RemainingCategories = existingRemovableCategories.ToArray();

                _projectCategoryModel.SelectedCategoryForProject = category;
                _projectCategoryModel.SelectedRemainingCategory = new KeyValuePair<int, string>(0, null);
            }
        }
    }

    public class RemoveCategoryFromProjet : ICommand
    {
        ProjectCategoryModel _projectCategoryModel;
        public RemoveCategoryFromProjet(ProjectCategoryModel projectCategoryModel)
        {
            _projectCategoryModel = projectCategoryModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            KeyValuePair<int, string> category = _projectCategoryModel.SelectedCategoryForProject;

            if (category.Key > 0)
            {
                List<KeyValuePair<int, string>> existingCategories = _projectCategoryModel.RemainingCategories.ToList();
                existingCategories.Add(category);
                _projectCategoryModel.RemainingCategories = existingCategories.ToArray();

                KeyValuePair<int, string> removableCategory = _projectCategoryModel.CategoriesForProject.Where(cp => cp.Key == category.Key).SingleOrDefault();
                List<KeyValuePair<int, string>> existingRemovableCategories = _projectCategoryModel.CategoriesForProject.ToList();
                existingRemovableCategories.Remove(removableCategory);
                _projectCategoryModel.CategoriesForProject = existingRemovableCategories.ToArray();

                _projectCategoryModel.SelectedRemainingCategory = category;
                _projectCategoryModel.SelectedCategoryForProject = new KeyValuePair<int, string>(0, null);
            }
        }
    }
}
