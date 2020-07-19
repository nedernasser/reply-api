using Reply.Exam.WebAPI.ViewModels;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Application.Customer
{
	public interface ICustomer
	{
		Task<CustomerModel> Add(CustomerModel model);
		Task<CustomerModel> GetById(int id);
		Task<CustomerModel> GetByCPF(string cpf);
	}
}
