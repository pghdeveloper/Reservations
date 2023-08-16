using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace Reservations.API.Models;

[Serializable]
[Table("Schedules")]
public class Provider
{
    [Key]
    [JsonIgnore]
    public int ScheduleId { get; set; }
    public int ProviderId { get; set; }
    public string ProviderExternalId { get; set; }
    public string StartDateTime { get; set; }
    public string StartEndTime { get; set; }
}