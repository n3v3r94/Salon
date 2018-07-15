using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Salon.Data.Models;




namespace Salon.Data
{
    public class SalonDbContext : IdentityDbContext<User>
    {
        public SalonDbContext(DbContextOptions<SalonDbContext> options)
            : base(options)
        {
        }

        public DbSet<Salons> Salons { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
         {

            builder.Entity<Salons>().HasMany(p => p.Products).WithOne(s => s.Salon).HasForeignKey(fk => fk.SalonId);

            base.OnModelCreating(builder);
           
        }
    }
}
