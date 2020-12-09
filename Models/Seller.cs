using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.Models
{
    public class Seller
    {
        public int Id { get; set; }
        public string BusinessName { get; set; }
        public string InChargePerson { get; set; }
        public string Country { get; set; }
        public string Location { get; set; }
        public string Address { get; set; }
        public string Telephone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Web { get; set; }
        public double PlatformCharge { get; set; }
        public string RegistredDate { get; set; }
        public string BannerImg { get; set; }
        public string ProfileImg { get; set; }
        public string SalesGoodType { get; set; }
        public string Description { get; set; }

    }
}
