using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using ShoppingCart.Models;
using ShoppingCart.Repos.Interface;
using ShoppingCart.Views;

namespace ShoppingCart.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGenericRepository<UserModel> _genericRepository;
        private readonly IPageDialogService _pageDialogService;
        private string _userDetails;
        private string _password;
        public LoginPageViewModel(INavigationService navigationService, IGenericRepository<UserModel> genericRepository, IPageDialogService pageDialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _genericRepository = genericRepository;
            _pageDialogService = pageDialogService;
            LoginCommand = new DelegateCommand(LoginCommandHandler);
            AdminLoginCommand = new DelegateCommand(AdminLoginCommandHandler);
            SignupCommand = new DelegateCommand(SignupCommandHandler);
        }

        public DelegateCommand LoginCommand { get; set; }
        public DelegateCommand SignupCommand { get; set; }
        public DelegateCommand AdminLoginCommand { get; set; }
        public string UserDetails { get { return _userDetails; } set { SetProperty(ref _userDetails, value); } }
        public string Password { get { return _password; } set { SetProperty(ref _password, value); } }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            UserDetails =string.Empty;
            Password =string.Empty;
        }
        private async void LoginCommandHandler()
        {
            if (UserDetails != null || Password != null)
            {
                var result = await _genericRepository.Get();
                var user = result.Where(x => x.UserName == UserDetails && x.Password == Password).FirstOrDefault();
                if (user != null)
                {
                    var parameters = new NavigationParameters { { "UserDetails", user } };
                    await _navigationService.NavigateAsync(nameof(ProductsPage),parameters);
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Alert", "Wrong Credentials", "Retry");
                }
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Enter your username and password", "Retry");

            }

        }
        private async void SignupCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(SignupPage));
        }
        private async void AdminLoginCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(AdminLoginPage));
        }
    }
}
