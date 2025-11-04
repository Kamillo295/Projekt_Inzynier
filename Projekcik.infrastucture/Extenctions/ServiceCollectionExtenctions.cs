using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projekcik.Infrastructure.Persistance;

namespace Projekcik.infrastucture.Extenctions
{
    public static class ServiceCollectionExtenctions
    {
        public static void AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddDbContext<AplicationDbContext>(options =>options.UseSqlServer(
                configuration.GetConnectionString("BazaRoboty")));

        }
    }
}
