using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Infrastructure.Models
{
	public class CustomerInsurance : BaseModel
	{
		public int CustomerID { get; set; }
		public int VehicleID { get; set; }
		public decimal InsuredValue { get; set; }
		public string VehiclePlate { get; set; }

		public Customer Customer { get; set; }
		public Vehicle Vehicle { get; set; }
	}
}
