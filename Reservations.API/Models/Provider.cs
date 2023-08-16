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
    public string ProviderExternalId { get; set; }
    public string FirstName{ get; set; }
    public string LastName { get; set; }
}