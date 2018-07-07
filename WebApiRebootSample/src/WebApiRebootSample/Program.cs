using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System.Diagnostics;

namespace WebApiRebootSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var process = Process.GetCurrentProcess();
            var processid = process.Id;
            //System.Threading.Mutex

            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
