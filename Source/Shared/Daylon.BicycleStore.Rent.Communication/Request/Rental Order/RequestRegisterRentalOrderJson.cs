using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Communication.Request
{
    public class RequestRegisterRentalOrderJson
    {
        // Time Properties
        public int RentalDays { get; set; }

        // Order Properties
        public PaymentMethodEnum PaymentMethod { get; set; }

        // Bicycle Properties
        public Guid BicycleId { get; set; }
    }
}
