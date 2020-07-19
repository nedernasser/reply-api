using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reply.Exam.WebAPI.Infrastructure;
using Reply.Exam.WebAPI.ViewModels;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Application.Vehicle
{
	public class Vehicle : IVehicle
	{
		private readonly ZurichContext _dbContext;
		private readonly IMapper _mapper;

		public Vehicle(
			ZurichContext dbContext,
			IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<VehicleModel> Add(VehicleModel model)
		{
			var domain = _mapper.Map<Infrastructure.Models.Vehicle>(model);
			_dbContext.Vehicles.Add(domain);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<VehicleModel>(domain);
		}

		public async Task<VehicleModel> GetByBrandModel(string brand, string model)
		{
			var domain = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Brand == brand && x.Model == model);
			return _mapper.Map<VehicleModel>(domain);
		}
	}
}
