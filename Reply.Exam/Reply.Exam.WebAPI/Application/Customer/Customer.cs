using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Reply.Exam.WebAPI.Infrastructure;
using Reply.Exam.WebAPI.ViewModels;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Application.Customer
{
	public class Customer : ICustomer
	{
		private readonly ZurichContext _dbContext;
		private readonly IMapper _mapper;

		public Customer(
			ZurichContext dbContext,
			IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}

		public async Task<CustomerModel> Add(CustomerModel model)
		{
			var domain = _mapper.Map<Infrastructure.Models.Customer>(model);
			_dbContext.Customers.Add(domain);
			await _dbContext.SaveChangesAsync();
			return _mapper.Map<CustomerModel>(domain);
		}

		public async Task<CustomerModel> GetById(int id)
		{
			var domain = await _dbContext.Customers.FirstOrDefaultAsync(x => x.ID == id);
			return _mapper.Map<CustomerModel>(domain);
		}

		public async Task<CustomerModel> GetByCPF(string cpf)
		{
			var domain = await _dbContext.Customers.FirstOrDefaultAsync(x => x.CPF == cpf);
			return _mapper.Map<CustomerModel>(domain);
		}
	}
}
