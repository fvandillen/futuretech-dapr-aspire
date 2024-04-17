using Dapr;
using Futuretech.Domain.Events;
using Futuretech.Services.RegulatoryInspector.Database;
using Microsoft.AspNetCore.Mvc;

namespace Futuretech.Services.RegulatoryInspector.Controllers;

[ApiController]
[Route("opening-time")]
public class OpeningTimeController(RegulatoryInspectorContext db, ILogger<OpeningTimeController> logger) : ControllerBase
{
    [Topic("pubsub", "airport-status-changed", "event.data.newStatus == true", 1)]
    [HttpPost("inspect")]
    public async Task<IActionResult> InspectAirportOpening(CloudEvent<AirportStatusChangedEvent> @event)
    {
        if (@event.Data.TimeChanged.Hour is 11)
        {
            logger.LogWarning("Airport violated the law: cannot be open between 22:00 and 07:00");
            db.Violations.Add(new Violation()
            {
                DateTime = DateTime.Now,
                Description = "Airport violated the law: cannot be open between 22:00 and 07:00"
            });
            await db.SaveChangesAsync();
        }
        
        return Ok();
    }
}