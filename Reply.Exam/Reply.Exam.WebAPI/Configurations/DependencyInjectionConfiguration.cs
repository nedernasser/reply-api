using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reply.Exam.WebAPI.Application.Vehicle;
using Reply.Exam.WebAPI.Application.CustomerInsurance;
using Reply.Exam.WebAPI.Application.Customer;

namespace Reply.Exam.WebAPI.Configurations
{
	public static class DependencyInjectionConfiguration
	{
		public static IServiceCollection AddDIConfiguration(this IServiceCollection services, IConfiguration configuration)
		{
			var settingConfiguration = new SettingConfiguration();
			new ConfigureFromConfigurationOptions<SettingConfiguration>(
				configuration.GetSection("SettingConfiguration"))
					.Configure(settingConfiguration);
			services.AddSingleton(settingConfiguration);

			services.AddScoped<ICustomer, Customer>();
			services.AddScoped<ICustomerInsurance, CustomerInsurance>();
			services.AddScoped<IVehicle, Vehicle>();

			return services;
		}
	}
}
