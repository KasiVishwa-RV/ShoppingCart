using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ShoppingCart.Models;
using ShoppingCart.Repos.Interface;
using SQLite;

namespace ShoppingCart.Repos
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private SQLiteAsyncConnection db;

        public GenericRepository()
        {
            var connection = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "LocalDB.db3");
            db = new SQLiteAsyncConnection(connection);
            db.CreateTableAsync<T>().Wait();
        }

        public async Task<List<T>> Get()
        {
            return await db.Table<T>().ToListAsync();
        }

        public async Task<int> Insert(T entity)
        {

            return await db.InsertAsync(entity);
        }

        public async Task<int> Update(T entity) =>
             await db.UpdateAsync(entity);

        public async Task<int> DeleteCart(CartModel entity)
        {
            var x = await db.FindAsync<CartModel>(entity.ProductId);
            return await db.DeleteAsync(x);
        }

        public async Task<int> DeleteUser(UserModel entity)
        {
            var x = await db.FindAsync<UserModel>(entity.Email);
            return await db.DeleteAsync(x);
        }
    }
}
