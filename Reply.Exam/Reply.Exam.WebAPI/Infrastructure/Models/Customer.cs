using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Infrastructure.Models
{
	public class Customer : BaseModel
	{
		public string Name { get; set; }
		public string CPF { get; set; }
		public int Age { get; set; }
	}
}
