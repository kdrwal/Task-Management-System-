
using TaskManagerWPF.Models.Services;
using TaskManagerWPF.Models.Dtos;
using CommunityToolkit.Mvvm.Messaging;
using TaskManagerWPF.Helpers;
using TaskManagerWPF.ViewModels.Single;

namespace TaskManagerWPF.ViewModels.Many;

public class UserListViewModel
    : BaseManyViewModel<UserService, UserDto, User>
{
    public UserListViewModel() : base("User List")
    {
    }

    protected override void ClearFilters()
    {
        SearchInput = null;
        SearchInput = null;
    }

    
    protected override void CreateNew()
    {
        WeakReferenceMessenger.Default.Send<OpenViewMessage>(new OpenViewMessage()
        {
            Sender = this,
            ViewModelToBeOpened = new AddUserViewModel()
        }); 
    }

  
}
