using Reply.Exam.WebAPI.Infrastructure.Models;

namespace Reply.Exam.WebAPI.Infrastructure.EntityConfigurations
{
	public class VehicleConfiguration : BaseModelConfiguration<Vehicle>
	{
		public VehicleConfiguration() : base("Vehicle") { }
	}
}
