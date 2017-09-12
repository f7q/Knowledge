using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace ef.Models
{
    public class CoreExampleContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }
        
        public CoreExampleContext()
        {

        }

        public CoreExampleContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            IConfiguration Configuration = builder.Build();
            var dbkind = Configuration["Data:DefaultConnection:ConnectionDBString"];
            if (dbkind.Equals("sqlite"))
            {
                optionsBuilder.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]);
            }
            if (dbkind.Equals("sqlserver"))
            {
                optionsBuilder.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);

            }
            if (dbkind.Equals("postgresql"))
            {
                optionsBuilder.UseNpgsql(Configuration["Data:DefaultConnection:ConnectionString"]);
            }
            if (dbkind.Equals("inmemory"))
            {
                optionsBuilder.UseInMemoryDatabase();
            }
        }
    }
}