namespace Reservations.API.Models.Requests;

public class ScheduleRequest
{
    public string ProviderExternalId { get; set; }
    public string StartDateTime { get; set; }
    public string EndDateTime { get; set; }
}