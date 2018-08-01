using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salon.Data.Models;
using Salon.Services;
using Salon.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salon.Web.Controllers
{
    public class SalonController : Controller
    {
        //za da raboti trqbva da registrame service v startup 
        private readonly ISalonServices salonSvc;

        public SalonController(ISalonServices salon)
        {
            this.salonSvc = salon;
           
        }



        [HttpGet]
        public IActionResult All()
        {

            return View(salonSvc.AllSalon());
        }

        [Authorize(Roles = "Salon")]
        public IActionResult Create ()
        {
            
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Create(Salons salon)
        {
            if (ModelState.IsValid)
            {

                var userName = HttpContext.User.Identity.Name;
                this.salonSvc.Create(salon , userName);
                return RedirectToAction(nameof(All));
            }
            return View(salon);
        }

        [Authorize(Roles = "Salon")]
        public IActionResult Edit(int  id )
        {

            var currentSalon = this.salonSvc.FindSalon(id);
            return View(currentSalon);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Edit(Salons salons, int id)
        {

            if (ModelState.IsValid)
            {

                salonSvc.Edit(salons, id);
                return RedirectToAction(nameof(All));
            }

            return View();
        }

        [Authorize(Roles = "Salon")]
        public IActionResult Delete(int id)
        {

            var currentSalon = this.salonSvc.FindSalon(id);

            return View(currentSalon);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Delete(int id, string str)
        {

            this.salonSvc.Delete(id,str);
            

            return RedirectToAction(nameof(All));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            var currentSalon = this.salonSvc.Details(id);

            return View(currentSalon);
        }

        [HttpGet]
        public IActionResult AddProduct(int id)
        {
            return View(new AddProductView () { SalonId = id });
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult AddProduct(AddProductView product,int id)
        {
            this.salonSvc.AddProduct(product, id);

            return RedirectToAction(nameof(MySalon));
        }

        [HttpGet]
        public IActionResult SearchByProduct(string product)
        {
            var result = this.salonSvc.SearchProduct(product);

            return View(result);
        }

        [Authorize(Roles = "Salon")]
        public IActionResult MySalon()
        {
            var userName = HttpContext.User.Identity.Name;

            return View(this.salonSvc.MySalons(userName));
        }

        [HttpGet]
        public IActionResult ProductWithWorkers(int id)
        {
            return View(this.salonSvc.GetProductWithWorkers(id));
        }

        public IActionResult ProductDetails(int id)
        {
            return View(this.salonSvc.ProductDetails(id));
        }
         


    }

  
}
