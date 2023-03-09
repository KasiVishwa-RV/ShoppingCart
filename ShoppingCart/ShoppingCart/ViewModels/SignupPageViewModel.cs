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
    public class SignupPageViewModel : ViewModelBase
    {
        private readonly IGenericRepository<UserModel> _genericRepository;
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private string _username;
        private string _password;
        private string _email;
        public SignupPageViewModel(INavigationService navigationService, IGenericRepository<UserModel> genericRepository,IPageDialogService pageDialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _genericRepository = genericRepository;
            _pageDialogService = pageDialogService;
            SignupCommand = new DelegateCommand(SignupCommandHandler);
        }
        public string Username { get { return _username; } set { SetProperty(ref _username, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }
        public string Email { get { return _email; } set { SetProperty(ref _email, value); } }
        public DelegateCommand SignupCommand { get; set; }
        private async void SignupCommandHandler()
        {
            if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password))
            {
                await _genericRepository.Insert(new UserModel { Email = Email, UserName = Username, Password = Password });
                await _navigationService.GoBackAsync();
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Enter Valid Details", "Ok");
            }
        }
    }
}
