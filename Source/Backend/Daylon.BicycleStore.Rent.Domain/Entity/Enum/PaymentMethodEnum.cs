using System.Text.Json.Serialization;

namespace Daylon.BicycleStore.Rent.Domain.Entity.Enum
{

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum PaymentMethodEnum
    {
        Indefinite = 0,
        Cash = 1,
        CreditCard = 2,
        DebitCard = 3,
        InstantTransfer = 4, // Pix, MB Way...
    }
}
