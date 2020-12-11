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
                new Seller(){ Id=1,BusinessName = "Ape kade",InChargePerson = "Delushaan", Country = EnumCountry.Singapore,Location = "WoodLand",Address = "Choa Chu Kang Ave 3",Telephone = "0000000000", Mobile = "1111111111",Email = "delushaan@outlook.com",PlatformCharge = 0.5,RegistredDate = DateTime.Now.ToString(),BannerImg = "~img/banner",ProfileImg = "~img/profile",SalesGoodType = EnumSalesGoodType.Tea, Description = "This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer."},
                new Seller(){ Id=2, BusinessName = "Ape Kade 2",InChargePerson = "Ruchiranga",Country = EnumCountry.Dubai,Location = "Torrento",Address = "Choa Chu Kang Ave 3",Telephone = "0000000000",Mobile = "1111111111",Email = "delushaan@outlook.com", PlatformCharge = 0.5,RegistredDate = DateTime.Now.ToString(),BannerImg = "~img/banner",ProfileImg = "~img/profile",SalesGoodType = EnumSalesGoodType.Clouth, Description = "This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer."}
            };
        }

        public Seller AdmimUpdateSeller(Seller seller)
        {
            Seller sellerModel = _sellerList.FirstOrDefault(e => e.Id == seller.Id);
            if (sellerModel != null)
            {
                sellerModel.BusinessName        = seller.BusinessName;
                sellerModel.InChargePerson      = seller.InChargePerson;
                sellerModel.Country             = seller.Country;
                sellerModel.Location            = seller.Location;
                sellerModel.Address             = seller.Address;
                sellerModel.Telephone           = seller.Telephone;
                sellerModel.Mobile              = seller.Mobile;
                sellerModel.Email               = seller.Email;
                sellerModel.Web                 = seller.Web;
                sellerModel.PlatformCharge      = seller.PlatformCharge;
                sellerModel.RegistredDate       = seller.RegistredDate;
                sellerModel.BannerImg           = seller.BannerImg;
                sellerModel.ProfileImg          = seller.ProfileImg;
                sellerModel.SalesGoodType       = seller.SalesGoodType;
                sellerModel.Description         = seller.Description;
            }

            return seller;
        }

        public Seller AdminCreateSeller(Seller seller)
        {
            seller.Id = _sellerList.Max(e => e.Id) + 1;
            _sellerList.Add(seller);
            return seller;
        }

        public Seller AdminDeleteSeller(int Id)
        {
            Seller seller = _sellerList.FirstOrDefault(e => e.Id == Id);
            if(seller != null)
            {
                _sellerList.Remove(seller);
            }

            return seller;
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
