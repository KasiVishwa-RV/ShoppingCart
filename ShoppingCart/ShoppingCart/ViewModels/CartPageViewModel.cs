using System;
using System.Linq;
using System.Threading.Tasks;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using ShoppingCart.Models;
using ShoppingCart.Repos.Interface;
using ShoppingCart.Views;
using Xamarin.CommunityToolkit.ObjectModel;

namespace ShoppingCart.ViewModels
{
    public class CartPageViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly IPageDialogService _pageDialogService;
        private readonly IGenericRepository<CartModel> _genericRepository;
        private ObservableRangeCollection<CartModel> _cartlist;
        private int _totalCost;

        public CartPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IGenericRepository<CartModel> genericRepository) : base(navigationService)
        {
            _navigationService = navigationService;
            _pageDialogService = pageDialogService;
            _genericRepository = genericRepository;
            PayCommand = new DelegateCommand(PayCommandHandler);
            RemoveCommand = new DelegateCommand<CartModel>(RemoveCommandHandler);
        }
        public ObservableRangeCollection<CartModel> CartList { get { return _cartlist; } set { SetProperty(ref _cartlist, value); } }
        public DelegateCommand PayCommand { get; set; }
        public DelegateCommand<CartModel> RemoveCommand { get; set; }
        public int TotalCost { get { return _totalCost; } set { SetProperty(ref _totalCost, value); } }
        public override async void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);
            await GetProducts();
        }
        private async Task GetProducts()
        {
            var data = await _genericRepository.Get();
            CartList = new ObservableRangeCollection<CartModel>(data);
            TotalCost = CartList.Sum(c => c.ProductPrice);
        }
        private async void RemoveCommandHandler(CartModel removedProduct)
        {
            try
            {
                await _genericRepository.DeleteCart(removedProduct);
                await GetProducts();
            }
            catch (Exception ex)
            {

            }
        }
        private async void PayCommandHandler()
        {
            var response = await _pageDialogService.DisplayAlertAsync("Confirm Payment", "Did you verify the payment?", "Yes", "No");
            if (response)
            {
                await _pageDialogService.DisplayAlertAsync("Successful", "Payment is Done", "Return to Products Page");
                await _navigationService.NavigateAsync(nameof(ProductsPage));
            }
        }
    }
}
