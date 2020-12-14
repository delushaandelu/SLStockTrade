using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.ViewModel
{
    public class AdminEditSellerViewModel : AdminCreateSellerViewModel
    {
        public int Id { get; set; }
        public string ExisitingProfilePhotoPath { get; set; }
        public string ExisitingBannerPhotoPath { get; set; }
    }
}
