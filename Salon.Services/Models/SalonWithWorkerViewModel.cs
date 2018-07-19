using Salon.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Salon.Services.Models
{
   public class SalonWithWorkerViewModel
    {
        public Salons Salons { get; set; }
        
        public List<User> WorkerUsers { get; set; }
    }
}
