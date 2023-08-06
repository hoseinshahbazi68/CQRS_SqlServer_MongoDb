using Data;
using System;
using System.Net;
using System.Text;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace WebFramework.Configuration
{
    public static class ServiceCollectionExtensions
    {

        public static void AddMainDbContext(this IServiceCollection services , IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options
                  .UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });


        }
    }
}
