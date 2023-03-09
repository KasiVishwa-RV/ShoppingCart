using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Http.Configuration;
using Newtonsoft.Json;
using ShoppingCart.Models;
using ShoppingCart.Services.Interfaces;

namespace ShoppingCart.Services
{
    public class ProductApiService : IProductApiService
    {
        public async Task<List<ProductModel>> GetProducts()
        {
            var apiCall = await "https://dummyjson.com/products".GetJsonAsync<Root>();
            var products = new List<ProductModel>(apiCall.Products);
            return products;
        }
    }
}
