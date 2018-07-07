using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using ef.Models;
using Microsoft.AspNetCore.Builder;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ef
{
    public class Program
    {
        /*
        private static DbContextOptions<CoreExampleContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<CoreExampleContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
        */

        private static IConfigurationRoot Configuration { get; set; }
        private static IHostingEnvironment HostingEnvironment { get; set; }

        public static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            //IServiceCollection services = new ServiceCollection();

            Console.WriteLine("Hello World!");
            var service = new ServiceCollection();

            var dbkind = Configuration["Data:DefaultConnection:ConnectionDBString"];
            if (dbkind.Equals("sqlite"))
            {
                service.AddEntityFrameworkSqlite();
                service.AddDbContext<CoreExampleContext>(options =>
                {
                    options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if (dbkind.Equals("sqlserver"))
            {
                service.AddEntityFrameworkSqlServer();
                service.AddDbContext<CoreExampleContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if (dbkind.Equals("postgresql"))
            {
                service.AddEntityFrameworkNpgsql();
                service.AddDbContext<CoreExampleContext>(options =>
                {
                    options.UseNpgsql(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if (dbkind.Equals("inmemory"))
            {
                service.AddEntityFrameworkInMemoryDatabase();
                service.AddDbContext<CoreExampleContext>(options =>
                {
                    options.UseInMemoryDatabase(Guid.NewGuid().ToString());
                });
            }

            var serviceProvider = service.BuildServiceProvider();
            // Setup Databases
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CoreExampleContext>();
                context.Database.EnsureCreated();
                //context.Database.EnsureDeleted();
                //context.Database.Migrate();
                //EnsureSeedData(serviceScope.ServiceProvider.GetService<CoreExampleContext>());
            }
        }
        /*
        public static void EnsureDatabaseCreated()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            if (HostingEnvironment.IsDevelopment()) optionsBuilder.UseSqlServer(Configuration["Data:dev:DataContext"]);
            else if (HostingEnvironment.IsStaging()) optionsBuilder.UseSqlServer(Configuration["Data:staging:DataContext"]);
            else if (HostingEnvironment.IsProduction()) optionsBuilder.UseSqlServer(Configuration["Data:live:DataContext"]);
            var context = new CoreExampleContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            optionsBuilder = new DbContextOptionsBuilder();
            if (HostingEnvironment.IsDevelopment()) optionsBuilder.UseSqlServer(Configuration["Data:dev:TransientContext"]);
            else if (HostingEnvironment.IsStaging()) optionsBuilder.UseSqlServer(Configuration["Data:staging:TransientContext"]);
            else if (HostingEnvironment.IsProduction()) optionsBuilder.UseSqlServer(Configuration["Data:live:TransientContext"]);
            new CoreExampleContext(optionsBuilder.Options).Database.EnsureCreated();
        }
        */
    }
}
