
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
    using Microsoft.AspNetCore.Mvc.Rendering;

    

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


        public void AddProduct(AddProductView product, int id)
        {
            var result = this.db.Salons.Include(p => p.Products).SingleOrDefault(s => s.Id == id);

            Product tempProdduct = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Discount = product.Discount
            };

            result.Products.Add(tempProdduct);
           
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


        //Anonymouse
        public ProductWithWorkers GetProductWithWorkers(int id)
        {
            //var product = this.db.Products.Include(s => s.Workers).Where(p => p.Id == id);
            
            ProductWithWorkers workers = new ProductWithWorkers();

            var productVersion2 = this.db.WorkerProduct.Include(p => p.Product).ThenInclude(w =>w.Workers).Where(p => p.Product.Id == id);

            foreach (var prod in productVersion2)
            {
                //TO DO fix override name
                workers.ProductName = prod.Product.Name;
                
                var worker = this.db.Worker.SingleOrDefault(w => w.Id == prod.WorkerId);
                var user = userManager.FindByIdAsync(worker.userId).Result;
                var workerName = user.Email;
                workers.Workers.Add(workerName);
                workers.selectWorker.Add(new SelectListItem
                {
                    Value = workerName,
                    Text = workerName
                });
            }

           // foreach (var worker in workers)
           // {
           // 
           //     var user = userManager.FindByIdAsync(worker.WorkerName).Result;
           // 
           //     ProductWithWorkers productWithWorkers = new ProductWithWorkers();
           //     productWithWorkers.ProductName = worker.ProductName;
           //     productWithWorkers.Workers.Add(user.Email);
           //     //TO DO optimize fix bug wih product name
           //     allWorkers.Add(productWithWorkers);
           //         
           // }
           //

            return workers;
        }

        //Salon Role
        public Product ProductDetails(int id)
        {
            var product = this.db.Products.SingleOrDefault(p => p.Id == id);

            return product;
        }

    }
}
