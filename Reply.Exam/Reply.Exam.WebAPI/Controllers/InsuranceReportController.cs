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
    public class InsuranceReportController : BaseController
    {
        private readonly ILogger<CustomerInsuranceController> _logger;
        private readonly ICustomerInsurance _customerInsurance;

		public InsuranceReportController(
			ILogger<CustomerInsuranceController> logger,
			ICustomerInsurance customerInsurance)
		{
			_customerInsurance = customerInsurance;
			_logger = logger;
		}

		[HttpGet]
		[AllowAnonymous]
		[Route("InsuranceAverage")]
		[ProducesResponseType(typeof(decimal), (int)HttpStatusCode.OK)]
		[ProducesResponseType(typeof(BadRequestObjectResult), (int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> Get()
		{
			decimal model;
			try
			{
				model = await _customerInsurance.GetInsuranceAverage();
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