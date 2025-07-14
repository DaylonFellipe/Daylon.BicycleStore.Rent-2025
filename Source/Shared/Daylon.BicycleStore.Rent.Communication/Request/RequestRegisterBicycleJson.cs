using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Communication.Request
{
    public class RequestRegisterBicycleJson
    {
        // Basic Properties
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        // Technical Properties
        public BrandEnum Brand { get; set; }
        public ModelEnum Model { get; set; }
        public ColorEnum Color { get; set; }

        // Stock Properties
        public double Price { get; set; }
        public int Quantity { get; set; }

        // Order Properties
        public double DailyRate { get; set; }
    }
}
