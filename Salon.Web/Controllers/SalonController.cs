using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Salon.Data.Models;
using Salon.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salon.Web.Controllers
{
    public class SalonController : Controller
    {
        //za da raboti trqbva da registrame service v startup 
        private readonly ISalonServices salon;

       

        public SalonController(ISalonServices salon)
        {
            this.salon = salon;
           
        }

        public IActionResult All()
        {

            return View(salon.AllSalon());
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
                this.salon.Create(salon , userName);
                return RedirectToAction(nameof(All));
            }
            return View(salon);
        }

        [Authorize(Roles = "Salon")]
        public IActionResult Edit(int  id )
        {

            var currentSalon = this.salon.FindSalon(id);
            return View(currentSalon);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Edit(Salons salons, int id)
        {

            if (ModelState.IsValid)
            {

                salon.Edit(salons, id);
                return RedirectToAction(nameof(All));
            }

            return View();
        }

        [Authorize(Roles = "Salon")]
        public IActionResult Delete(int id)
        {

            var currentSalon = this.salon.FindSalon(id);

            return View(currentSalon);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult Delete(int id, string str)
        {

            this.salon.Delete(id,str);
            

            return RedirectToAction(nameof(All));
        }

        public IActionResult Details(int id)
        {
            var currentSalon = this.salon.Details(id);

            return View(currentSalon);
        }

        public IActionResult AddProduct()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize(Roles = "Salon")]
        public IActionResult AddProduct(Product product)
        {
            this.salon.AddProduct(product);

            return RedirectToAction(nameof(All));
        }

        public IActionResult SearchByProduct(string product)
        {
            var result = this.salon.SearchProduct(product);

            return View(result);
        }

        [Authorize(Roles = "Salon")]
        public IActionResult MySalon()
        {
            var userName = HttpContext.User.Identity.Name;

            return View(this.salon.MySalons(userName));
        }




    }

  
}
