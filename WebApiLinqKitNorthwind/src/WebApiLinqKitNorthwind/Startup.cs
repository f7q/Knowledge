using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using WebApiScaffoldNorthwind.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiScaffoldNorthwind
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
                services.AddDbContext<PostgresContext>(options =>
                {
                    options.UseSqlite(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if(dbkind.Equals("sqlserver"))
            {
                services.AddEntityFrameworkSqlServer();
                services.AddDbContext<PostgresContext>(options =>
                {
                    options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if(dbkind.Equals("postgresql"))
            {
                services.AddEntityFrameworkNpgsql();
                services.AddDbContext<PostgresContext>(options =>
                {
                    options.UseNpgsql(Configuration["Data:DefaultConnection:ConnectionString"]);
                });
            }
            if(dbkind.Equals("inmemory"))
            {
                services.AddEntityFrameworkInMemoryDatabase();
                services.AddDbContext<PostgresContext>(options =>
                {
                    options.UseInMemoryDatabase();
                });
            }
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1",
                    new Info
                    {
                        Version = "v1",
                        Title = "Geo Search API",
                        Description = "A simple api to search using geo location in Elasticsearch",
                        TermsOfService = "None"
                    }
                 );

                //var filePath = Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "MyApi.xml");
                //c.IncludeXmlComments(filePath);
                //c.IncludeXmlComments(pathToDoc);
                c.DescribeAllEnumsAsStrings();
            });

            //services.AddAutoMapper();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();

            app.UseSwagger(c =>
            {
                c.RouteTemplate = "api-docs/{documentName}/swagger.json";
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/api-docs/v1/swagger.json", "My API V1");
            });
        }
    }
}
