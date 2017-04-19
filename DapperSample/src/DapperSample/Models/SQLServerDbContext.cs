using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace WebApiSample.Models
{
    public class SQLServerDbContext : DbContext
    {
        public SQLServerDbContext(DbContextOptions<SQLServerDbContext> options) :base(options)
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
                if(dbkind.Equals("sqlserver"))
                {
                    optionsBuilder.UseSqlServer(config["Data:DefaultConnection:ConnectionString"]);
                }
            }
        }

        public DbSet<Value> Values { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<Value>().ToTable("Values");
            builder.Entity<Value>().HasKey(m => m.Id); // この場合、自動採番がデフォルトで、ID無しで登録になる。
            base.OnModelCreating(builder);
        }
    }
}
