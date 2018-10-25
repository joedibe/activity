using Microsoft.EntityFrameworkCore;
using OnlineActivity.EFCore.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineActivity.EFCore.Infra
{
    public class OnlineActivityDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public DbSet<Document> Documents { get; set; }

        public OnlineActivityDbContext()
        {

        }

        public OnlineActivityDbContext(DbContextOptions<OnlineActivityDbContext> options) : base(options)
        {
                
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Server=.;Database=OnlineActivityDB;Integrated Security=true;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
