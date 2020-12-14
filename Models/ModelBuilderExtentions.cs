using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SL_StockTrade.Models
{
    public static class ModelBuilderExtentions
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Seller>().HasData(
                    new Seller
                    {
                        Id = 2,
                        BusinessName = "Masala Shop",
                        InChargePerson = "Ruchiragna",
                        Country = EnumCountry.Singapore,
                        Location = "Downtown",
                        Address = "Downtown center",
                        Telephone = "1111111111",
                        Mobile = "0000000000",
                        Email = "delushaan@outlook.com",
                        Web = "www.google.com",
                        PlatformCharge = 5.8,
                        RegistredDate = DateTime.Today.ToString(),
                        BannerImg = "img",
                        ProfileImg = "prof",
                        SalesGoodType = EnumSalesGoodType.Tea,
                        Description = "some textsome textsome textsome textsome textsome text"
                    },
                    new Seller
                    {
                        Id = 3,
                        BusinessName = "Ruchiragna Masala Shop",
                        InChargePerson = "Ruchiragna",
                        Country = EnumCountry.Singapore,
                        Location = "Downtown",
                        Address = "Downtown center",
                        Telephone = "1111111111",
                        Mobile = "0000000000",
                        Email = "delushaan@outlook.com",
                        Web = "www.google.com",
                        PlatformCharge = 5.8,
                        RegistredDate = DateTime.Today.ToString(),
                        BannerImg = "img",
                        ProfileImg = "prof",
                        SalesGoodType = EnumSalesGoodType.Tea,
                        Description = "some textsome textsome textsome textsome textsome text"
                    }
                );
        }
    }
}
