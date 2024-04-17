using Dapr.Client;
using Microsoft.AspNetCore.Mvc;

namespace Futuretech.Services.Flight.Controllers;

[ApiController]
public class SchedulerController(DaprClient daprClient, ILogger<SchedulerController> logger) : ControllerBase
{
	[HttpPost("~/scheduler")]
	public async Task<IActionResult> ScheduleFlights()
	{
		logger.LogInformation("Attempting to schedule flights, checking if airport is open");
		// First, check if the airport is open.
		var request = daprClient.CreateInvokeMethodRequest(HttpMethod.Get, "airport", "status");
		var response = await daprClient.InvokeMethodAsync<bool>(request);

		if (response)
		{
			logger.LogInformation("Airport is open. Scheduling flights");
		}
		else
		{
			logger.LogWarning("Airport is closed. Not scheduling flights");
		}
		
		return Ok();
	}
}