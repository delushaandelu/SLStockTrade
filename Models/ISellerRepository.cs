using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.Models
{
    public interface ISellerRepository
    {
        Seller GetSeller(int Id);
        IEnumerable<Seller> GetAllSellers();
        Seller AdminCreateSeller(Seller seller);
    }
}
