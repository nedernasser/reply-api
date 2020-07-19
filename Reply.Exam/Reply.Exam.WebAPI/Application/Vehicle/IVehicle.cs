using Reply.Exam.WebAPI.ViewModels;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Application.Vehicle
{
	public interface IVehicle
	{
		Task<VehicleModel> Add(VehicleModel model);
		Task<VehicleModel> GetByBrandModel(string brand, string model);
	}
}
