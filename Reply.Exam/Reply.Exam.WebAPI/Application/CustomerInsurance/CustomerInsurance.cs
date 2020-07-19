using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reply.Exam.WebAPI.Configurations;
using Reply.Exam.WebAPI.Infrastructure;
using Reply.Exam.WebAPI.ViewModels;
using System.Threading.Tasks;
using System.Linq;

namespace Reply.Exam.WebAPI.Application.CustomerInsurance
{
	public class CustomerInsurance : ICustomerInsurance
	{
		private readonly SettingConfiguration _settingConfiguration;
		private readonly ZurichContext _dbContext;
		private readonly IMapper _mapper;

		public CustomerInsurance(
			SettingConfiguration settingConfiguration,
			ZurichContext dbContext,
			IMapper mapper)
		{
			_settingConfiguration = settingConfiguration;
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CustomerInsuranceModel> Add(CustomerInsuranceModel model)
		{
			var domain = _mapper.Map<Infrastructure.Models.CustomerInsurance>(model);
			domain.Vehicle = await _dbContext.Vehicles.FirstOrDefaultAsync(x => x.Brand == model.Vehicle.Brand && x.Model == model.Vehicle.Model);

			var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CPF == domain.Customer.CPF);
			if (customer == null)
				_dbContext.Customers.Add(domain.Customer);
			else
				domain.Customer = customer;

			domain.VehicleID = domain.Vehicle.ID;
			domain.CustomerID = domain.Customer.ID;

			var riskRate = ((domain.Vehicle.Value * 5) / (domain.Vehicle.Value * 2)) / 100;
			var riskValue = riskRate * domain.Vehicle.Value;
			var pureInsurance = riskValue * (1 + _settingConfiguration.SafetyMargin);
			var insuranceValue = pureInsurance + (pureInsurance * _settingConfiguration.Profit);
			domain.InsuredValue = insuranceValue;

			var existingDomain = await _dbContext.CustomersInsurance.FirstOrDefaultAsync(x => x.VehiclePlate == domain.VehiclePlate && x.CustomerID == domain.CustomerID && x.VehicleID == domain.VehicleID);
			if (existingDomain != null)
			{
				domain = existingDomain;
				domain.InsuredValue = insuranceValue;
				_dbContext.CustomersInsurance.Update(domain);
			}
			else
				_dbContext.CustomersInsurance.Add(domain);

			await _dbContext.SaveChangesAsync();
			return _mapper.Map<CustomerInsuranceModel>(domain);
		}

		public async Task<CustomerInsuranceModel> GetByVehiclePlate(string vehiclePlate)
		{
			var query = from ci in _dbContext.CustomersInsurance
						join c in _dbContext.Customers on ci.CustomerID equals c.ID
						join v in _dbContext.Vehicles on ci.VehicleID equals v.ID
						where ci.VehiclePlate.Equals(vehiclePlate)
						select new Infrastructure.Models.CustomerInsurance
						{
							ID = ci.ID,
							CustomerID = ci.CustomerID,
							VehicleID = ci.VehicleID,
							VehiclePlate = ci.VehiclePlate,
							InsuredValue = ci.InsuredValue,
							Customer = c,
							Vehicle = v
						};

			var domain = await query.FirstOrDefaultAsync();
			return _mapper.Map<CustomerInsuranceModel>(domain);
		}

		public async Task<decimal> GetInsuranceAverage()
		{
			var domain = await _dbContext.CustomersInsurance.ToListAsync();
			return domain.Sum(x => x.InsuredValue) / domain.Count;
		}
	}
}
