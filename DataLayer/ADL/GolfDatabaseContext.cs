using DataLayer.DataModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.ADL
{
    public class GolfDatabaseContext: DbContext
    {
        public GolfDatabaseContext(DbContextOptions<GolfDatabaseContext> options): base(options)
        {
            
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderComponent> OrdersComponent { get; set; }
        public DbSet<Component> Components { get; set; }


    }
}

