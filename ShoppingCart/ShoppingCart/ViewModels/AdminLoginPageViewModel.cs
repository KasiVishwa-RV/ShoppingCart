using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ShoppingCart.Views;

namespace ShoppingCart.ViewModels
{
    public class AdminLoginPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private string _adminUsername;
        private string _adminPassword;
        public AdminLoginPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            LoginCommand = new DelegateCommand(LoginCommandHandler);
        }
        public DelegateCommand LoginCommand { get; set; }
        public string AdminUsername { get { return _adminUsername; } set { SetProperty(ref _adminUsername, value); } }
        public string AdminPassword { get { return _adminPassword; } set { SetProperty(ref _adminPassword, value); } }
        private async void LoginCommandHandler()
        {
            if (AdminUsername == "Admin" && AdminPassword == "admin@123")
            {
                await _navigationService.NavigateAsync(nameof(AdminPage));
            }
            else
            {
                await _pageDialogService.DisplayAlertAsync("Alert", "Enter Admin Credentials", "Ok");
            }
        }
    }
}
