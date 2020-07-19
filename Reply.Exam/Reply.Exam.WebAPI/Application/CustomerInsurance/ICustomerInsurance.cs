using Reply.Exam.WebAPI.ViewModels;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Application.CustomerInsurance
{
	public interface ICustomerInsurance
	{
		Task<CustomerInsuranceModel> Add(CustomerInsuranceModel model);
		Task<CustomerInsuranceModel> GetByVehiclePlate(string vehiclePlate);
		Task<decimal> GetInsuranceAverage();
	}
}
