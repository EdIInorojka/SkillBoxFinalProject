using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using MyCompanyWebAPI.Domain;

namespace MyCompanyWebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    }
}
