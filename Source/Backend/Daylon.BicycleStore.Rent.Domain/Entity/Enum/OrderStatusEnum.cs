using System.Text.Json.Serialization;

namespace Daylon.BicycleStore.Rent.Domain.Entity.Enum
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum OrderStatusEnum
    {
        Indefinite = 0,
        Rented = 1,
        Overdue = 2,
        Returned = 3,
    }
}
