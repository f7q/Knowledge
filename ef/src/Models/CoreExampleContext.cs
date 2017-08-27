using Microsoft.EntityFrameworkCore;

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
            //optionsBuilder.UseNpgsql(@"Host=localhost;Port=5432;Username=postgres;Password=;Database=postgres;");
            //optionsBuilder.UseSqlServer(@"Server=tcp:rdexampledb1svr.database.windows.net,1433;Initial Catalog=RdExampleDb1;Persist Security Info=False;User ID=[user];Password=[password];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=aspnetcore;Trusted_Connection=True;MultipleActiveResultSets=true");
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\ProjectsV13;Database=aspnetcore;Trusted_Connection=True;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlite(@"Data Source=db.sqlite");
        }
    }
}