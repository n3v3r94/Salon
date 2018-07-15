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

        public IActionResult Create ()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult Create(Salons salon)
        {
            if (ModelState.IsValid)
            {
                this.salon.Create(salon);
                return RedirectToAction(nameof(All));
            }
            return View(salon);
        }

        public IActionResult Edit(int  id )
        {

            var currentSalon = this.salon.FindSalon(id);
            return View(currentSalon);
        }

        [HttpPost]
        public IActionResult Edit(Salons salons, int id)
        {

            if (ModelState.IsValid)
            {

                salon.Edit(salons, id);
                return RedirectToAction(nameof(All));
            }

            return View();
        }

        public IActionResult Delete(int id)
        {

            var currentSalon = this.salon.FindSalon(id);

            return View(currentSalon);
        }

        [HttpPost]
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
    }

  
}
