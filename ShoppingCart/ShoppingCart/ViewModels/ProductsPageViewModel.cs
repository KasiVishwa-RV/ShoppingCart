using System;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ShoppingCart.Models;
using ShoppingCart.Repos.Interface;
using ShoppingCart.Services.Interfaces;
using ShoppingCart.Views;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ShoppingCart.ViewModels
{
    public class ProductsPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IProductApiService _productApiService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IGenericRepository<CartModel> _genericRepository;
        private ObservableRangeCollection<ProductModel> _productList;
        public ProductsPageViewModel(INavigationService navigationService, IProductApiService productApiService, IPageDialogService pageDialogService, IGenericRepository<CartModel> genericRepository) : base(navigationService)
        {
            _navigationService = navigationService;
            _productApiService = productApiService;
            _genericRepository = genericRepository;
            _pageDialogService = pageDialogService;
            OpenCartCommand = new DelegateCommand(OpenCartCommandHandler);
            AddToCartCommand = new DelegateCommand<ProductModel>(AddToCartCommandHandler);
            LogoutCommand = new DelegateCommand(LogoutCommandHandler);
        }

        public ObservableRangeCollection<ProductModel> ProductList { get => _productList; set => SetProperty(ref _productList, value); }

        public DelegateCommand GetProducts { get; set; }
        public DelegateCommand OpenCartCommand { get; set; }
        public DelegateCommand LogoutCommand { get; set; }
        public DelegateCommand<ProductModel> AddToCartCommand { get; set; }
        public async override void OnNavigatedTo(INavigationParameters parameters)
        {
            try
            {
                base.OnNavigatedTo(parameters);
                var products = await _productApiService.GetProducts();
                ProductList = new ObservableRangeCollection<ProductModel>(products);
            }
            catch (Exception ex)
            {
            }

        }
        private async void AddToCartCommandHandler(ProductModel product)
        {
            try
            {
                var cartItem = new CartModel { ProductTitle = product.Title, ProductBrand = product.Brand, ProductPrice = product.Price, ProductId = product.Id };
                if (await AvoidDuplicate(cartItem))
                {
                    await _genericRepository.Insert(cartItem);
                }
                else
                {
                    await _pageDialogService.DisplayAlertAsync("Alert", "Item Already Exists", "Ok");
                }
            }
            catch (Exception ex)
            {

            }
        }
        private async Task<bool> AvoidDuplicate(CartModel product)
        {
            var data = await _genericRepository.Get();
            if (data.Contains(product))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private void LogoutCommandHandler()
        {
            _navigationService.GoBackToRootAsync();
        }
        private async void OpenCartCommandHandler()
        {
            await _navigationService.NavigateAsync(nameof(CartPage));
        }
    }
}
