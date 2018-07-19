using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Data.Models
{
  public  class ProductWorker
    {
         public int ProductId { get; set; }

        public Product Product { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
