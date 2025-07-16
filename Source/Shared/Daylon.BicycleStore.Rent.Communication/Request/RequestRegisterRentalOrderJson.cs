using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Communication.Request
{
    public class RequestRegisterRentalOrderJson
    {
        // Time Properties
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }
        public int RentalDays { get; set; }
        public DateTime DropOffTime { get; set; }

        // Order Properties
        public PaymentMethodEnum PaymentMethod { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }

        // Bicycle Properties
        public Guid BicycleId { get; set; }
    }
}
