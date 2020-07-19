using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Reply.Exam.WebAPI.Infrastructure.EntityConfigurations;
using Reply.Exam.WebAPI.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Reply.Exam.WebAPI.Infrastructure
{
	public class ZurichContext : DbContext
	{
		public ZurichContext(DbContextOptions<ZurichContext> options) : base(options)
		{
		}

		public DbSet<Customer> Customers { get; set; }
		public DbSet<Vehicle> Vehicles { get; set; }
		public DbSet<CustomerInsurance> CustomersInsurance { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfiguration(new CustomerConfiguration());
			builder.ApplyConfiguration(new VehicleConfiguration());
			builder.ApplyConfiguration(new CustomerInsuranceConfiguration());
		}
	}
}
