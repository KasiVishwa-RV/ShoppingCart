using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ShoppingCart.Models;
using SQLite;

namespace ShoppingCart.Repos.Interface
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<List<T>> Get();
        Task<int> Insert(T entity);
        Task<int> Update(T entity);
        Task<int> DeleteCart(CartModel entity);
        Task<int> DeleteUser(UserModel entity);
    }
}
