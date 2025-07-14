using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Domain.Entity
{
    public class BicycleForRent
    {
        // Basic Properties
        public Guid OrderId { get; set; }

        // Time Properties
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }

        // Order Properties
        public int RentalDays { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }

        // Bicycle Properties
        public Guid BicycleId { get; set; }
        public required Bicycle Bicycle { get; set; }
    }
}
