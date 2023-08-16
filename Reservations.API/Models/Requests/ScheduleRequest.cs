namespace Reservations.API.Models.Requests;

public class ScheduleRequest
{
    public string ProviderExternalId { get; set; }
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
}