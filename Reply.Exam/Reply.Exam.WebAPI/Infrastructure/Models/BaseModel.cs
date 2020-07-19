using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Infrastructure.Models
{
	public class BaseModel
	{
		public int ID { get; set; }
        public bool Deleted { get; set; }
	}
}
