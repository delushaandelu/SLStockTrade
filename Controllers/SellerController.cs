using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using SL_StockTrade.Models;
using SL_StockTrade.ViewModel;

namespace SL_StockTrade.Controllers
{
    public class SellerController : Controller
    {
        //Constructor Injection
        private readonly ISellerRepository _sallesRepository;
        private readonly IHostingEnvironment hostingEnvironment;

        public SellerController(ISellerRepository sallesRepository, IHostingEnvironment hostingEnvironment)
        {
            _sallesRepository = sallesRepository;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult Index()
        {
            var model = _sallesRepository.GetAllSellers();
            return View(model);
        }
              
        public ViewResult Details(int id)
        {
            SellerDetailsViewModel sellerDetailsViewModel = new SellerDetailsViewModel()
            {
                Seller = _sallesRepository.GetSeller(id),
                PageTitle = "Seller Details"
            };

            return View(sellerDetailsViewModel);
        }

        //Admin Side Controls
        public ViewResult AdminIndex()
        {
            var model = _sallesRepository.GetAllSellers();
            return View(model);
        }

        public ViewResult AdminDetails(int id)
        {
            SellerDetailsViewModel sellerDetailsViewModel = new SellerDetailsViewModel()
            {
                Seller = _sallesRepository.GetSeller(id),
                PageTitle = "Seller Details"
            };

            return View(sellerDetailsViewModel);
        }

        [HttpGet]
        public ViewResult AdminCreateSeller()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AdminCreateSeller(AdminCreateSellerViewModel model)
        {
            if(ModelState.IsValid)
            {
                string uniqueFileName = null;
                string uniqueBannerName = null;

                if(model.ProfilePic != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "img/Profile");
                    uniqueFileName =  Guid.NewGuid().ToString() + "_" + model.ProfilePic.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueFileName);
                    model.ProfilePic.CopyTo(new FileStream(filePath, FileMode.Create));
                }

                if (model.BannerPic != null)
                {
                    string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "img/Cover");
                    uniqueBannerName = Guid.NewGuid().ToString() + "_" + model.BannerPic.FileName;
                    string filePath = Path.Combine(uploadFolder, uniqueBannerName);
                    model.BannerPic.CopyTo(new FileStream(filePath, FileMode.Create));
                }


                Seller newSeller = new Seller
                {
                    BusinessName = model.BusinessName,
                    InChargePerson = model.InChargePerson,
                    Country = model.Country,
                    Location = model.Location,
                    Address = model.Address,
                    Telephone = model.Telephone,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Web = model.Web,
                    PlatformCharge = model.PlatformCharge,
                    RegistredDate = model.RegistredDate,
                    BannerImg = uniqueBannerName,
                    ProfileImg = uniqueFileName,
                    SalesGoodType = model.SalesGoodType,
                    Description = model.Description
                };

                _sallesRepository.AdminCreateSeller(newSeller);
                return RedirectToAction("AdminDetails", new { id = newSeller.Id });
            }

            return View();
        }
    }
}
