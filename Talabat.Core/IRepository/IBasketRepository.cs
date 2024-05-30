using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.IRepository
{
    public interface IBasketRepository
    {
        Task <CustomerBasket ?> GetBasketAsync (string id);
        Task<bool> DeleteBasketAsync (string id);
        Task<CustomerBasket?> CreateOrUpdateAsync (CustomerBasket basket);
    }
}
