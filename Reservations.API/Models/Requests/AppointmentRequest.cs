namespace Reservations.API.Models.Requests;

public class AppointmentRequest
{
    public string ClientExternalId { get; set; } = string.Empty;
    public string ScheduleExternalId { get; set; } = string.Empty;
    public DateTime AppointmentDateTime { get; set; }
    
}