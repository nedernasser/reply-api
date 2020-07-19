using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Reply.Exam.WebAPI.Application.Customer;
using Reply.Exam.WebAPI.Application.CustomerInsurance;
using Reply.Exam.WebAPI.Application.Vehicle;
using Reply.Exam.WebAPI.ViewModels;

namespace Reply.Exam.WebAPI.Controllers
{
	[ApiController]
	public class CustomerInsuranceController : BaseController
	{
		private readonly ILogger<CustomerInsuranceController> _logger;
		private readonly ICustomerInsurance _customerInsurance;
		private readonly IVehicle _vehicle;

		public CustomerInsuranceController(
			ILogger<CustomerInsuranceController> logger,
			ICustomerInsurance customerInsurance,
			IVehicle vehicle)
		{
			_customerInsurance = customerInsurance;
			_vehicle = vehicle;
			_logger = logger;
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("Insurance")]
		[ProducesResponseType(typeof(CustomerInsuranceModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Get(string vehiclePlate)
		{
			CustomerInsuranceModel model;
			try
			{
				model = await _customerInsurance.GetByVehiclePlate(vehiclePlate);
				if (model == null)
					return NotFound("Seguro não encontrado");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex);
			}

			return Ok(model);
		}

		[HttpPost]
		[AllowAnonymous]
		[Route("Insurance")]
		[ProducesResponseType(typeof(CustomerInsuranceModel), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Post(CustomerInsuranceModel model)
		{
			try
			{
				if (model.Customer == null)
					return BadRequest("Segurado é obrigatório");
				if (model.Customer != null &&
					(string.IsNullOrEmpty(model.Customer.Name) ||
					string.IsNullOrEmpty(model.Customer.CPF)))
					return BadRequest("Os dados do segurado são obrigatório (Nome e CPF)");
				if (model.Vehicle == null)
					return BadRequest("Veículo é obrigatório");
				if (model.Vehicle != null &&
					(string.IsNullOrEmpty(model.Vehicle.Brand) ||
					string.IsNullOrEmpty(model.Vehicle.Model) ||
					string.IsNullOrEmpty(model.VehiclePlate)))
					return BadRequest("Os dados do veículo são obrigatório (Marca e Modelo)");
				var isValidVehicle = await _vehicle.GetByBrandModel(model.Vehicle.Brand, model.Vehicle.Model) != null;
				if (!isValidVehicle)
					return NotFound("Veículo não encontrado");

				model = await _customerInsurance.Add(model);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, ex.Message);
				return BadRequest(ex);
			}

			return Ok(model);
		}
	}
}
