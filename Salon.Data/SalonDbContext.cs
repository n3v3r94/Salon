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
        public DbSet<User> DbUsers { get; set; }
        

        protected override void OnModelCreating(ModelBuilder builder)
         {

            builder.Entity<Salons>().HasMany(p => p.Products).WithOne(s => s.Salon).HasForeignKey(fk => fk.SalonId);

            builder.Entity<Salons>().HasOne(u => u.User).WithMany(s => s.Salon).HasForeignKey(u => u.UserId);

            builder.Entity<Salons>().Property(s => s.RowVersion).IsConcurrencyToken();

            builder.Entity<Product>().Property(s => s.RowVersion).IsConcurrencyToken();

            builder.Entity<ProductWorker>().HasKey(pw => new { pw.ProductId, pw.UserId });

            builder.Entity<ProductWorker>().HasOne(u => u.User).WithMany(p => p.Products).HasForeignKey(f => f.UserId);

            builder.Entity<ProductWorker>().HasOne(p => p.Product).WithMany(u => u.SalonWorkers).HasForeignKey(f => f.ProductId);

            base.OnModelCreating(builder);
           
        }
    }
}
