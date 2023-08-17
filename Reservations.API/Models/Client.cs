using System.Text.Json.Serialization;
using Dapper.Contrib.Extensions;

namespace Reservations.API.Models;

[Serializable]
[Table("Clients")]
public class Client
{
    [Key]
    [JsonIgnore]
    public int ClientId { get; set; }

    public string ClientExternalId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}