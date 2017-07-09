using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Model;
using WebApiSample.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiSample
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IServiceProvider srv)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            srv.GetService<ILoggerFactory>().AddConsole(Configuration.GetSection("Logging"));
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();
            
            var dbkind = Configuration["Data:DefaultConnection:ConnectionDBString"];
            if(dbkind.Equals("sqlite"))
            {
                services.AddEntityFrameworkSqlite();
                services.AddDbContext<SampleDbContext>(options =>
                {
                    options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if(dbkind.Equals("sqlserver"))
            {
                services.AddEntityFrameworkSqlServer();
                services.AddDbContext<SampleDbContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if(dbkind.Equals("postgresql"))
            {
                services.AddEntityFrameworkNpgsql();
                services.AddDbContext<SampleDbContext>(options =>
                {
                    options.UseNpgsql(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if(dbkind.Equals("inmemory"))
            {
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddDbContext<SampleDbContext>(options =>
                {
                    options.UseInMemoryDatabase();
                });
            }
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(options =>
            {
                options.SingleApiVersion(new Info
                {
                    Version = "v1",
                    Title = "Geo Search API",
                    Description = "A simple api to search using geo location in Elasticsearch",
                    TermsOfService = "None"
                });
                //options.IncludeXmlComments(pathToDoc);
                options.DescribeAllEnumsAsStrings();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi();
        }
    }
}
