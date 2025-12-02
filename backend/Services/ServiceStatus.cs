using System.Text.Json.Serialization;

namespace backend.Services
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // to show proper enum in swagger
    public enum ServiceStatus
    {

        available,
        unavailable
    }
}
