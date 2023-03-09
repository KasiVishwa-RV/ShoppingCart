using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace ShoppingCart.Models
{
    public class CartModel
    {
        [PrimaryKey]
        public int ProductId { get; set; }
        public string ProductTitle { get; set; }
        public string ProductBrand { get; set; }
        public int ProductPrice { get; set; }
    }
}
