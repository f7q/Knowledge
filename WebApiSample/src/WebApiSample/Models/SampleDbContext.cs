using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace WebApiSample.Models
{
    public class SampleDbContext : DbContext
    {
        public SampleDbContext(DbContextOptions<SampleDbContext> options) :base(options)
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var dbkind = config["Data:DefaultConnection:ConnectionDBString"];
                if(dbkind.Equals("sqlite"))
                {
                    optionsBuilder.UseSqlite(config["Data:DefaultConnection:ConnectionString"]);
                }
                if (dbkind.Equals("sqlserver"))
                {
                    optionsBuilder.UseSqlServer(config["Data:DefaultConnection:ConnectionString"]);
                }
                if (dbkind.Equals("postgresql"))
                {
                    optionsBuilder.UseNpgsql(config["Data:DefaultConnection:ConnectionString"]);
                }
                if (dbkind.Equals("inmemory"))
                {
                    //optionsBuilder.UseInMemoryDatabase();
                    base.OnConfiguring(optionsBuilder);
                }
            }

            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<Value> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Value>().HasKey(m => m.Id);
            base.OnModelCreating(builder);
        }
    }
}
