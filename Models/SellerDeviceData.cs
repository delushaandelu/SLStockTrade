using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.Models
{
    public class SellerDeviceData : IdentityUser
    {
        public string deviceUsername { get; set; }
        public string ipaddress { get; set; }
        public string deviceLocation { get; set; }
    }
}
