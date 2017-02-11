using Microsoft.EntityFrameworkCore;

namespace ef.Models
{
    public class CoreExampleContext : DbContext
    {
        public DbSet<Company> Company { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=tcp:rdexampledb1svr.database.windows.net,1433;Initial Catalog=RdExampleDb1;Persist Security Info=False;User ID=[user];Password=[password];MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            optionsBuilder.UseSqlite(@"Data Source=db.sqlite");
        }
    }
}