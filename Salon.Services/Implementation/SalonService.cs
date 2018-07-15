
namespace Salon.Services.Implementation
{

    using System.Collections.Generic;
    using Salon.Services.Models;
    using Data;
    using System.Linq;
    using Salon.Data.Models;
    using Microsoft.EntityFrameworkCore;
    using System;

    public class SalonService : ISalonServices
    {
        private readonly SalonDbContext db;

        public SalonService(SalonDbContext db)
        {
            this.db = db;
        }

        public IEnumerable<Salons> AllSalon()
        {
            var salon = this.db.Salons.ToList();
            return salon;

            //modela ot bazata e sashtiq kato view modela i nqma smisal ot dolniq Kod :D
           // return salon.Select(s => new SalonModel
           // {
           //     Name = s.Name,
           //     City = s.City,
           //     Country = s.Country,
           //     Id = s.Id,
           //     Products = s.Products
           //  }).ToList(); ;
        }

        public void Create(Salons salon)
        {
           
            this.db.Salons.Add(salon);
            db.SaveChanges();
        }


        public Salons FindSalon(int id)
        {
            var salonFromDb = this.db.Salons.FirstOrDefault(s => s.Id == id);
            return salonFromDb;

          
        }
        public void Edit (Salons salon, int id)
        {
           try
                {
                    var salonFromDb = this.db.Salons.FirstOrDefault(s => s.Id == id);
                    salonFromDb.Name = salon.Name;
                    salonFromDb.City = salon.City;
                    salonFromDb.Country = salon.Country;
                    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                    salon.Products = salon.Products;

                    db.SaveChanges();
                }
               
                catch (DbUpdateConcurrencyException ex)
                {
                   Console.WriteLine("Ne vlizai tuk");
                }
        }


        public void Delete( int id )
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
            var salon = db.Salons.SingleOrDefault(s => s.Id == id);

            return (salon);
        }

    }
}
