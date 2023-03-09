using System;
using System.Collections.Generic;
using System.Linq;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using ShoppingCart.Models;
using ShoppingCart.Repos.Interface;
using ShoppingCart.Views;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ShoppingCart.ViewModels
{
    public class AdminPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IGenericRepository<UserModel> _genericRepository;
        private ObservableRangeCollection<UserModel> _userData;
        public AdminPageViewModel(INavigationService navigationService, IGenericRepository<UserModel> genericRepository) : base(navigationService)
        {
            _navigationService = navigationService;
            _genericRepository = genericRepository;
            EditCommand = new DelegateCommand<UserModel>(EditCommandHandler);
            UserData = new ObservableRangeCollection<UserModel>();
        }
        public ObservableRangeCollection<UserModel> UserData { get { return _userData; } set { SetProperty(ref _userData, value); } }
        public DelegateCommand<UserModel> EditCommand { get; set; }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            if (parameters.GetNavigationMode() == NavigationMode.New)
            {
                var userDetails = await _genericRepository.Get();
                if (userDetails != null)
                {
                    UserData = new ObservableRangeCollection<UserModel>(userDetails);
                }
            }
        }
        private void EditCommandHandler(UserModel userModel)
        {
            var parameters = new NavigationParameters { { "UserData", userModel } };
            _navigationService.NavigateAsync(nameof(UserDetailsEditPopup), parameters, useModalNavigation: true);
        }
    }
}
