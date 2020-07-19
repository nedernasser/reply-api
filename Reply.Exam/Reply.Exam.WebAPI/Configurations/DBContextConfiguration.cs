using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reply.Exam.WebAPI.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Configurations
{
	public static class DBContextConfiguration
	{
        public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ZurichContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                        sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                    });
            }, ServiceLifetime.Transient);

            return services;
        }
    }
}
