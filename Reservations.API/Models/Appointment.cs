using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace Reservations.API.Models;

[Serializable]
[Table("Appointments")]
public class Appointment
{
    [Key]
    [JsonIgnore]
    public int AppointmentId { get; set; }
    public int ScheduleId { get; set; }
    public int ClientId { get; set; }
    public string AppointmentDateTime { get; set; } = string.Empty;
    public string CreatedDateTime { get; set; } = string.Empty;
    public string ExpirationDateTime { get; set; } = string.Empty;
    public bool Confirmed { get; set; }
    public bool Expired { get; set; }
    public string AppointmentExternalId { get; set; } = string.Empty;
}