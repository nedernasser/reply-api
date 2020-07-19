using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Infrastructure.Models
{
	public class Vehicle : BaseModel
	{
		public string Brand { get; set; }
		public string Model { get; set; }
		public decimal Value { get; set; }
	}
}
