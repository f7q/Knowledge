using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;
using scaffold.Models;
using Microsoft.AspNetCore.Builder;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace scaffold
{
    public class Program
    {
        private static DbContextOptions<postgresContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<postgresContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }
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
                service.AddDbContext<postgresContext>(options =>
                {
                    options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if (dbkind.Equals("sqlserver"))
            {
                service.AddEntityFrameworkSqlServer();
                service.AddDbContext<postgresContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if (dbkind.Equals("postgresql"))
            {
                service.AddEntityFrameworkNpgsql();
                service.AddDbContext<postgresContext>(options =>
                {
                    options.UseNpgsql(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if (dbkind.Equals("inmemory"))
            {
                service.AddEntityFrameworkInMemoryDatabase();
                service.AddDbContext<postgresContext>(options =>
                {
                    options.UseInMemoryDatabase();
                });
            }

            var serviceProvider = service.BuildServiceProvider();
            // Setup Databases
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<postgresContext>();
                context.Database.EnsureCreated();
                //context.Database.EnsureDeleted();
                //context.Database.Migrate();
                //EnsureSeedData(serviceScope.ServiceProvider.GetService<CoreExampleContext>());
            }
        }

        public static void EnsureDatabaseCreated()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            if (HostingEnvironment.IsDevelopment()) optionsBuilder.UseSqlServer(Configuration["Data:dev:DataContext"]);
            else if (HostingEnvironment.IsStaging()) optionsBuilder.UseSqlServer(Configuration["Data:staging:DataContext"]);
            else if (HostingEnvironment.IsProduction()) optionsBuilder.UseSqlServer(Configuration["Data:live:DataContext"]);
            var context = new postgresContext(optionsBuilder.Options);
            context.Database.EnsureCreated();

            optionsBuilder = new DbContextOptionsBuilder();
            if (HostingEnvironment.IsDevelopment()) optionsBuilder.UseSqlServer(Configuration["Data:dev:TransientContext"]);
            else if (HostingEnvironment.IsStaging()) optionsBuilder.UseSqlServer(Configuration["Data:staging:TransientContext"]);
            else if (HostingEnvironment.IsProduction()) optionsBuilder.UseSqlServer(Configuration["Data:live:TransientContext"]);
            new postgresContext(optionsBuilder.Options).Database.EnsureCreated();
        }
    }
}
