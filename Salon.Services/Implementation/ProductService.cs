using Salon.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Services.Implementation
{
   public class ProductService
    {
        private readonly SalonDbContext db;

        public ProductService(SalonDbContext db)
        {
            this.db = db;
        }

        public void EditProduct(int id)
        {

        }
    }
}
