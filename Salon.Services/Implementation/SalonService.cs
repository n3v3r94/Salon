
namespace Salon.Services.Implementation
{

    using System.Collections.Generic;
    using Salon.Services.Models;
    using Data;
    using System.Linq;
    using Salon.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Identity;
    using System.Security.Claims;
    using Microsoft.AspNetCore.Http;

    public class SalonService : ISalonServices
    {
        private readonly SalonDbContext db;
        private readonly UserManager<User> userManager;

        public SalonService(SalonDbContext db, UserManager<User> userManager)
        {
            this.db = db;
            this.userManager = userManager;

        }

        public IEnumerable<SalonViewModel> AllSalon()
        {

            var salon = db.Salons;
            //modela ot bazata e sashtiq kato view modela
            return salon.Select(s => new SalonViewModel
            {
                Name = s.Name,
                City = s.City,
                Country = s.Country,
                Id = s.Id,
                Products = s.Products
            }).ToList(); ;
        }

        public  void Create(Salons salon, string name)
        {
            var user = db.Users.Include(s => s.Salon).FirstOrDefault(n => n.Email == name);
            user.Salon.Add(salon);
            db.SaveChanges();
        }


        public Salons FindSalon(int id)
        {
            var salonFromDb = this.db.Salons.FirstOrDefault(s => s.Id == id);
            return salonFromDb;


        }
        public void Edit(Salons salon, int id)
        {
            try
            {
                var salonFromDb = this.db.Salons.FirstOrDefault(s => s.Id == id);
                salonFromDb.Name = salon.Name;
                salonFromDb.City = salon.City;
                salonFromDb.Country = salon.Country;
                salon.Products = salon.Products;

                db.SaveChanges();
            }

            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Ne vlizai tuk");
            }
        }


        public void Delete(int id)
        {
            var salon = db.Salons.SingleOrDefault(s => s.Id == id);

        }

        public void Delete(int id, string str)
        {
            var salon = db.Salons.SingleOrDefault(s => s.Id == id);
            db.Salons.Remove(salon);
            db.SaveChanges();
        }

        public Salons Details(int id)
        {
            var salon = db.Salons.Include("Products").SingleOrDefault(s => s.Id == id);

            return (salon);
        }


        public void AddProduct(Product product)
        {
            var result = this.db.Products;

            result.Add(product);
            db.SaveChanges();

        }

        public List<SearchByProductViewModel> SearchProduct(string product)
        {
            var result = this.db.Salons.Include(p => p.Products);

            List<SearchByProductViewModel> searchByProducts = new List<SearchByProductViewModel>(); ;
            foreach (var sal in result)
            {
                foreach (var currrentProduct in sal.Products)
                {
                    if (currrentProduct.Name == product)
                    {
                        var prod = new SearchByProductViewModel();
                        prod.Id = sal.Id;
                        prod.SalonName = sal.Name;
                        prod.ProductName = currrentProduct.Name;
                        searchByProducts.Add(prod);
                    }
                }

            }

            return searchByProducts;
        }

        public IEnumerable<SalonViewModel> MySalons(string name)
        {
            
            var mySalon = db.Users.Include(s => s.Salon).FirstOrDefault(n => n.Email == name);

            return mySalon.Salon.Select(s => new SalonViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Products = s.Products,
                City = s.City,
                Country = s.Country
                
            });
        }

        public void Book()
        {

        }
    }
}
