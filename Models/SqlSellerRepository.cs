using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.Models
{
    public class SqlSellerRepository : ISellerRepository
    {
        private readonly AppDbContext context;

        public SqlSellerRepository(AppDbContext context)
        {
            this.context = context;
        }

        public Seller AdmimUpdateSeller(Seller seller)
        {
            var sellerModel = context.Sellers.Attach(seller);
            sellerModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();

            return seller;
        }

        public Seller AdminCreateSeller(Seller seller)
        {
            context.Sellers.Add(seller);
            context.SaveChanges();
            return seller;
        }

        public Seller AdminDeleteSeller(int Id)
        {
            Seller seller =context.Sellers.Find(Id);
            if(seller != null)
            {
                context.Sellers.Remove(seller);
                context.SaveChanges();
            }

            return seller;
        }

        public IEnumerable<Seller> GetAllSellers()
        {
            return context.Sellers;
        }

        public Seller GetSeller(int Id)
        {
            return context.Sellers.Find(Id);
        }
    }
}
