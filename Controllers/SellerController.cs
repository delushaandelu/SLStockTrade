using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SL_StockTrade.Models;
using SL_StockTrade.ViewModel;

namespace SL_StockTrade.Controllers
{
    public class SellerController : Controller
    {
        //Constructor Injection
        private readonly ISellerRepository _sallesRepository;

        public SellerController(ISellerRepository sallesRepository)
        {
            _sallesRepository = sallesRepository;
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

        public ViewResult AdminCreateSeller()
        {
            return View();
        }
    }
}
