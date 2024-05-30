using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Orders
{
    public class OrderItem :BaseEntity
    {
       
        public productItemOrder  product {  get; set; } 
        
        public decimal price { get; set; }

        public int Quantity { get; set; }

        public OrderItem(productItemOrder product, decimal price, int quantity)
        {
            this.product = product;
            this.price = price;
            Quantity = quantity;
        }

        public OrderItem()
        {
            
        }
    }
}
