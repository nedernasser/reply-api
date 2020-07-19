using Microsoft.Extensions.DependencyInjection;
using Reply.Exam.WebAPI.Infrastructure.Models;
using Reply.Exam.WebAPI.ViewModels;
using AutoMapper;

namespace Reply.Exam.WebAPI.Configurations
{
	public static class AutoMapperConfiguration
	{
		public static void AddAutoMapper(this IServiceCollection services)
		{
			var config = new AutoMapper.MapperConfiguration(cfg =>
			{
				cfg.CreateMap<Customer, CustomerModel>();
				cfg.CreateMap<CustomerModel, Customer>();

				cfg.CreateMap<Vehicle, VehicleModel>();
				cfg.CreateMap<VehicleModel, Vehicle>();

				cfg.CreateMap<CustomerInsurance, CustomerInsuranceModel>();
				cfg.CreateMap<CustomerInsuranceModel, CustomerInsurance>();
			});
			IMapper mapper = config.CreateMapper();
			services.AddSingleton(mapper);
		}
	}
}
