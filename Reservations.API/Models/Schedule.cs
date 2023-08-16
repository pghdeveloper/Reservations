using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace Reservations.API.Models;

[Serializable]
[Table("Schedules")]
public class Schedule
{
    [Key]
    [JsonIgnore]
    public int ScheduleId { get; set; }
    public int ProviderId { get; set; }
    public string StartDateTime { get; set; }
    public string EndDateTime { get; set; }
}