using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ShoppingCart.Models;
using ShoppingCart.Repos.Interface;

namespace ShoppingCart.ViewModels
{
    public class UserDetailsEditPopupViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGenericRepository<UserModel> _genericRepository;
        private readonly IPageDialogService _pageDialogService;
        private bool _isPasswordNotVisible = true;
        private string _userName;
        private string _password;
        private string _email;
        private UserModel _userData;
        public UserDetailsEditPopupViewModel(INavigationService navigationService, IGenericRepository<UserModel> genericRepository, IPageDialogService pageDialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _genericRepository = genericRepository;
            _pageDialogService = pageDialogService;
            PasswordVisibilityCommand = new DelegateCommand(PasswordVisibilityCommandHandler);
            CancelCommand = new DelegateCommand(CancelCommandHandler);
            UpdateCommand = new DelegateCommand(UpdateCommandHandler);
            DeleteUserCommand = new DelegateCommand(DeleteUserCommandHandler);
        }
        public DelegateCommand PasswordVisibilityCommand { get; set; }
        public DelegateCommand CancelCommand { get; set; }
        public DelegateCommand UpdateCommand { get; set; }
        public DelegateCommand DeleteUserCommand { get; set; }
        public bool IsPasswordNotVisible { get { return _isPasswordNotVisible; } set { SetProperty(ref _isPasswordNotVisible, value); } }
        public string Username { get { return _userName; } set { SetProperty(ref _userName, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }
        public string Email { get { return _email; } set { SetProperty(ref _email, value); } }

        public override void OnNavigatedTo(INavigationParameters parameters)
        { 
            _userData = parameters.GetValue<UserModel>("UserData");
            Username = _userData.UserName;
            Password = _userData.Password;
            Email = _userData.Email;
        }
        private void PasswordVisibilityCommandHandler()
        {
            if (IsPasswordNotVisible == true)
                IsPasswordNotVisible = false;
            else
                IsPasswordNotVisible = true;
        }
        private async void CancelCommandHandler()
        {
            await _navigationService.GoBackAsync();
        }
        private async void UpdateCommandHandler()
        {
            _userData.UserName = Username;
            _userData.Password = Password;
            _userData.Email = Email;
            await _genericRepository.Update(_userData);
            await _pageDialogService.DisplayAlertAsync("Success", "User Details Updated", "Ok");
            await _navigationService.GoBackAsync();
        }
        private async void DeleteUserCommandHandler()
        {
            await _genericRepository.DeleteUser(_userData);
            await _pageDialogService.DisplayAlertAsync("Success", "User Details Deleted", "Ok");
            await _navigationService.GoBackAsync();

        }
    }
}
