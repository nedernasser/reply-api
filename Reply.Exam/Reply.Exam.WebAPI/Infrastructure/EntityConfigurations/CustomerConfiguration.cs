using Reply.Exam.WebAPI.Infrastructure.Models;

namespace Reply.Exam.WebAPI.Infrastructure.EntityConfigurations
{
	public class CustomerConfiguration : BaseModelConfiguration<Customer>
	{
		public CustomerConfiguration() : base("Customer") { }
	}
}
