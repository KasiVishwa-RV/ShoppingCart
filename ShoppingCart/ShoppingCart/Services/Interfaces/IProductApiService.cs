using System.Collections.Generic;
using System.Threading.Tasks;
using ShoppingCart.Models;

namespace ShoppingCart.Services.Interfaces
{
    public interface IProductApiService
    {
        Task<List<ProductModel>> GetProducts();
    }
}
