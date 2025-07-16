using Daylon.BicycleStore.Rent.Domain.Entity.Enum;
using System.ComponentModel.DataAnnotations;

namespace Daylon.BicycleStore.Rent.Domain.Entity
{
    public class RentalOrder
    {
        // Basic Properties
        [Key]
        public Guid OrderId { get; set; }

        // Time Properties
        public DateTime RentalStart { get; set; }
        public DateTime RentalEnd { get; set; }
        public int RentalDays { get; set; }
        public DateTime DropOffTime { get; set; } 


        // Order Properties
        public PaymentMethodEnum PaymentMethod { get; set; }
        public double TotalPrice { get; set; }
        public OrderStatusEnum OrderStatus { get; set; }

        // Bicycle Properties
        public Guid BicycleId { get; set; }
        public required Bicycle Bicycle { get; set; }
    }
}
