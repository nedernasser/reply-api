using Xunit;
using System;
using System.Net;
using System.Net.Http;
using Reply.Exam.WebAPI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Reply.Exam.WebAPI.ViewModels;
using System.Text;
using Newtonsoft.Json;

namespace Reply.Exam.UnitTests
{
	public class InsuranceTest
	{
		private readonly HttpClient _client;

		public InsuranceTest()
		{
			var configuration = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile("appsettings.json")
				.Build();

			var server = new TestServer(new WebHostBuilder()
				.UseConfiguration(configuration)
				.UseEnvironment("Development")
				.UseStartup<Startup>());
			_client = server.CreateClient();
		}

		[Fact]
		public void GetCustomerInsuranceOK()
		{
			var response = _client.GetAsync("/CustomerInsurance/Insurance?vehiclePlate=EUT5087").Result;
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public void GetCustomerInsuranceNotFound()
		{
			var response = _client.GetAsync("/CustomerInsurance/Insurance?vehiclePlate=ABC1234").Result;
			Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
		}

		[Fact]
		public void PostCustomerInsurance()
		{
			var json = JsonConvert.SerializeObject(new CustomerInsuranceModel()
			{
				VehiclePlate = "EUT5087",
				Customer = new CustomerModel()
				{
					Name = "Neder Nached Nasser",
					CPF = "30207385807"
				},
				Vehicle = new VehicleModel()
				{
					Brand = "Honda",
					Model = "CRV"
				}
			});
			var data = new StringContent(json, Encoding.UTF8, "application/json");
			var response = _client.PostAsync("/CustomerInsurance/Insurance", data).Result;
			Assert.Equal(HttpStatusCode.OK, response.StatusCode);
		}

		[Fact]
		public void GetInsurancesAverage()
		{
			var response = _client.GetAsync("/InsuranceReport/InsuranceAverage").Result;
			var result = Convert.ToDouble(response.Content.ReadAsStringAsync().Result);

			Assert.Equal(1081.5, result);
		}
	}
}
