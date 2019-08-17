using GolfAPI.DataLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GolfAPI.DataLayer.ADL
{
    public class GolfDatabaseContext: DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderComponent> OrdersComponent { get; set; }
        public DbSet<Component> Components { get; set; }

        public GolfDatabaseContext(DbContextOptions<GolfDatabaseContext> options): base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasOne(o => o.CreatedBy)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserForeignKey);

            //seed a user
            
            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id= 1,
                        Username = "morrissey",
                        Firstname = "Steven Patrick",
                        Lastname = "Morrissey",
                        Guid = new Guid(),
                        Role = RoleEnum.Manager,
                        Password = "admin123",
                        Orders = new List<Order>()
                    }
                );
            
        }

       


    }
}

