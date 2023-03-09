using System;
using System.IO;
using Flurl.Http;
using Flurl.Http.Configuration;
using Fusillade;
using LiteDB;
using Prism;
using Prism.Ioc;
using ShoppingCart.Models;
using ShoppingCart.Repos;
using ShoppingCart.Repos.Interface;
using ShoppingCart.Services;
using ShoppingCart.Services.Interfaces;
using ShoppingCart.ViewModels;
using ShoppingCart.Views;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;

namespace ShoppingCart
{
    public partial class App
    {
        public App(IPlatformInitializer initializer)
            : base(initializer)
        {
        }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            FlurlHttp.Configure(settings => settings.HttpClientFactory = new CustomHttpClientFactory());
            await NavigationService.NavigateAsync("NavigationPage/LoginPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<ILiteRepository>(new LiteRepository(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "cart.db")));
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();
            containerRegistry.RegisterSingleton<IProductApiService, ProductApiService>();
            containerRegistry.Register(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<LoginPage, LoginPageViewModel>();
            containerRegistry.RegisterForNavigation<ProductsPage, ProductsPageViewModel>();
            containerRegistry.RegisterForNavigation<SignupPage, SignupPageViewModel>();
            containerRegistry.RegisterForNavigation<AdminPage, AdminPageViewModel>();
            containerRegistry.RegisterForNavigation<AdminLoginPage, AdminLoginPageViewModel>();
            containerRegistry.RegisterForNavigation<UserDetailsEditPopup, UserDetailsEditPopupViewModel>();
            containerRegistry.RegisterForNavigation<CartPage, CartPageViewModel>();
        }
    }
}
