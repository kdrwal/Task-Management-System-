using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.Models.Contexts;
using TaskManagerWPF.Models.Dtos;
using TaskManagerWPF.Models.Services;

namespace TaskManagerWPF.ViewModels.Single
{
    public class AddUserViewModel
        : BaseCreateViewModel<UserService, UserDto, User>
    {
        private readonly DatabaseContext _db;
        public ICommand SaveCommand { get; set; }
        
        private ObservableCollection<Role> _roles;
        public ObservableCollection<Role> Roles
        {
            get => _roles;
            set
            {
                _roles = value;
                OnPropertyChanged(() => Roles);
            }
        }

       
        private Role _selectedRole;
        public Role SelectedRole
        {
            get => _selectedRole;
            set
            {
                if (_selectedRole != value)
                {
                    _selectedRole = value;
                    
                    Model.RoleId = value?.RoleId;
                    OnPropertyChanged(() => SelectedRole);
                }
            }
        }

        
        public string Username
        {
            get => Model.Username;
            set
            {
                if (Model.Username != value)
                {
                    Model.Username = value;
                    OnPropertyChanged(() => Username);
                }
            }
        }

        public string Email
        {
            get => Model.Email;
            set
            {
                if (Model.Email != value)
                {
                    Model.Email = value;
                    OnPropertyChanged(() => Email);
                }
            }
        }

        // Constructor
        public AddUserViewModel() : base("Add New User")
        {
            _db = new DatabaseContext();
            SaveCommand = new BaseCommand(() => Save());
            
            Model.IsActive = true;
            Model.CreatedAt = DateTime.Now;

            
            LoadRoles();
        }

        private void LoadRoles()
        {
            
            var rolesFromDb = _db.Roles
                                 .Where(r => r.DeletedAt == null)
                                 .ToList();

            Roles = new ObservableCollection<Role>(rolesFromDb);
        }

        
        public override void Save()
        {
            try
            {
               
                Service.AddModel(Model);

                OnRequestClose();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong:\n" + ex.ToString(), "Error");
            }
        }
    }
}
