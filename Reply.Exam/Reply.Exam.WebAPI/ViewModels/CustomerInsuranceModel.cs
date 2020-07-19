using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.ViewModels
{
	public class CustomerInsuranceModel
	{
		public int ID { get; set; }
		public int CustomerID { get; set; }
		public int VehicleID { get; set; }
		public decimal InsuredValue { get; set; }
		public string VehiclePlate { get; set; }
		public CustomerModel Customer { get; set; }
		public VehicleModel Vehicle { get; set; }
	}
}
