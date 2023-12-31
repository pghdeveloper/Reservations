using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace Reservations.API.Models;

[Serializable]
[Table("Providers")]
public class Provider
{
    [Key]
    [JsonIgnore]
    public int ProviderId { get; set; }

    public string ProviderExternalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}