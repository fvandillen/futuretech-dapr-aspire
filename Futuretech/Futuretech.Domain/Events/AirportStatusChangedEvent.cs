namespace Futuretech.Domain.Events;

public record AirportStatusChangedEvent(bool NewStatus, DateTime TimeChanged);