using System.Text.Json.Serialization;

namespace backend.Employees
{
    [JsonConverter(typeof(JsonStringEnumConverter))] // to show proper enum in swagger
    public enum EmployeeStatus
    {
        Active,
        Inactive
    }
}
