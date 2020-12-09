using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.Models
{
    public class MockSallerRepository : ISellerRepository
    {
        private List<Seller> _sellerList;

        public MockSallerRepository()
        {
            _sellerList = new List<Seller>()
            {
                new Seller(){ Id=1,BusinessName = "Ape Kade",InChargePerson = "Delushaan", Country = "Singapore",Location = "WoodLand",Address = "Choa Chu Kang Ave 3",Telephone = "0000000000", Mobile = "1111111111",Email = "delushaan@outlook.com",PlatformCharge = 0.5,RegistredDate = DateTime.Now.ToString(),BannerImg = "~img/banner",ProfileImg = "~img/profile",SalesGoodType = "spices"},
                new Seller(){ Id=2, BusinessName = "Ape Kade 2",InChargePerson = "Ruchiranga",Country = "Canada",Location = "Torrento",Address = "Choa Chu Kang Ave 3",Telephone = "0000000000",Mobile = "1111111111",Email = "delushaan@outlook.com", PlatformCharge = 0.5,RegistredDate = DateTime.Now.ToString(),BannerImg = "~img/banner",ProfileImg = "~img/profile",SalesGoodType = "spices"}
            };
        }

        public IEnumerable<Seller> GetAllSellers()
        {
            return _sellerList;
        }

        public Seller GetSeller(int Id)
        {
            return _sellerList.FirstOrDefault(e => e.Id == Id);
        }
    }
}
