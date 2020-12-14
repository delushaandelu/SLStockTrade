using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
            Seller seller = _sallesRepository.GetSeller(id);
            if(seller == null)
            {
                Response.StatusCode = 404;
                return View("ErrorNotFound", id);
            }

            SellerDetailsViewModel sellerDetailsViewModel = new SellerDetailsViewModel()
            {
                Seller = seller,
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
                string uniqueFileName = ProcessProfilPicture(model);
                string uniqueBannerName = ProcessBannerPicture(model);


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

        [HttpGet]
        public ViewResult AdminEditSeller(int id)
        {
            Seller seller = _sallesRepository.GetSeller(id);

            if (seller == null)
            {
                Response.StatusCode = 404;
                return View("ErrorNotFound", id);
            }

            AdminEditSellerViewModel adminEditSellerViewModel = new AdminEditSellerViewModel
            {
                Id = seller.Id,
                BusinessName = seller.BusinessName,
                InChargePerson = seller.InChargePerson,
                Country = seller.Country,
                Location = seller.Location,
                Address = seller.Address,
                Telephone = seller.Telephone,
                Mobile = seller.Mobile,
                Email = seller.Email,
                Web = seller.Web,
                PlatformCharge = seller.PlatformCharge,
                RegistredDate = seller.RegistredDate,
                ExisitingProfilePhotoPath = seller.ProfileImg,
                ExisitingBannerPhotoPath = seller.BannerImg,
                SalesGoodType = seller.SalesGoodType,
                Description = seller.Description
            };

            return View(adminEditSellerViewModel);
        }

        [HttpPost]
        public IActionResult AdminEditSeller(AdminEditSellerViewModel model)
        {
            if (ModelState.IsValid)
            {
                Seller seller = _sallesRepository.GetSeller(model.Id);
                seller.BusinessName = model.BusinessName;
                seller.InChargePerson = model.InChargePerson;
                seller.Country = model.Country;
                seller.Location = model.Location;
                seller.Address = model.Address;
                seller.Telephone = model.Telephone;
                seller.Mobile = model.Mobile;
                seller.Email = model.Email;
                seller.Web = model.Web;
                seller.PlatformCharge = model.PlatformCharge;
                seller.RegistredDate = model.RegistredDate;
                seller.BannerImg = model.ExisitingProfilePhotoPath;
                seller.ProfileImg = model.ExisitingProfilePhotoPath;
                seller.SalesGoodType = model.SalesGoodType;
                seller.Description = model.Description;
                if (model.ProfilePic != null)
                {
                    if(model.ExisitingProfilePhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "img/Profile", model.ExisitingProfilePhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    seller.ProfileImg=ProcessProfilPicture(model);
                }
                
                if(model.BannerPic != null)
                {
                    if (model.ExisitingBannerPhotoPath != null)
                    {
                        string filePath = Path.Combine(hostingEnvironment.WebRootPath, "img/Cover", model.ExisitingBannerPhotoPath);
                        System.IO.File.Delete(filePath);
                    }
                    seller.BannerImg = ProcessBannerPicture(model);
                }

                _sallesRepository.AdmimUpdateSeller(seller);
                return RedirectToAction("Index");
            }

            return View();
        }

        private string ProcessBannerPicture(AdminCreateSellerViewModel model)
        {
            string uniqueBannerName = null;


            if (model.BannerPic != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "img/Cover");
                uniqueBannerName = Guid.NewGuid().ToString() + "_" + model.BannerPic.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueBannerName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.BannerPic.CopyTo(fileStream);
                }
               
            }

            return uniqueBannerName;
        }

        private string ProcessProfilPicture(AdminCreateSellerViewModel model)
        {
            string uniqueFileName = null;

            if (model.ProfilePic != null)
            {
                string uploadFolder = Path.Combine(hostingEnvironment.WebRootPath, "img/Profile");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ProfilePic.FileName;
                string filePath = Path.Combine(uploadFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    model.ProfilePic.CopyTo(fileStream);
                }
            }

            return uniqueFileName;
        }
    }
}
