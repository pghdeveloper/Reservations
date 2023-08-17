namespace Reservations.API.Models.Response;

public class AppointmentResponse
{
    public string ClientExternalId { get; set; } = string.Empty;
    public string ScheduleExternalId { get; set; } = string.Empty;
    public DateTime AppointmentDateTime { get; set; }
    public string AppointmentExternalId { get; set; } = string.Empty;
    public bool Confirmed { get; set; }
    public bool Expired { get; set; }
}