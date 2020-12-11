using Microsoft.AspNetCore.Http;
using SL_StockTrade.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.ViewModel
{
    public class AdminCreateSellerViewModel
    {
        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "Business Owner Name")]
        public string InChargePerson { get; set; }

        [Required]
        public EnumCountry? Country { get; set; }

        [Required]
        public string Location { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string Telephone { get; set; }

        public string Mobile { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Format")]
        [Display(Name = "Business Email")]
        public string Email { get; set; }

        [Display(Name = "Business Website")]
        public string Web { get; set; }

        [Display(Name = "Platform Charge % per month")]
        public double PlatformCharge { get; set; }

        [Required]
        [Display(Name = "Business Started Since")]
        public string RegistredDate { get; set; }

        [Display(Name = "Profile Cover Image")]
        public string BannerImg { get; set; }

        [Display(Name = "Profile Image")]
        public IFormFile ProfilePic { get; set; }

        [Required]
        [Display(Name = "Selleing Good Type")]
        public EnumSalesGoodType? SalesGoodType { get; set; }

        [Required]
        [Display(Name = "Description for the Showcase")]
        [MaxLength(128, ErrorMessage = "Description can not exceed 128 chharacters!")]
        public string Description { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
