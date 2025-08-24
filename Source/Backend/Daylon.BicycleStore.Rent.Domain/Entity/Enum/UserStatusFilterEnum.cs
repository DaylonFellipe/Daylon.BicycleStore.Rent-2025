using System.Text.Json.Serialization;

namespace Daylon.BicycleStore.Rent.Domain.Entity.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserStatusFilterEnum
    {
        All = 0,
        Active = 1,
        Inactive = 2,
    }
}
