using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace Daylon.BicycleStore.Rent.Communication.Request
{
    public class RequestPatchBicycleJson
    {
        // Basic Properties
        public string? Name { get; set; } 
        public string? Description { get; set; } 

        // Technical Properties
        public BrandEnum? Brand { get; set; }
        public ModelEnum? Model { get; set; }
        public ColorEnum? Color { get; set; }

        // Stock Properties
        public double? Price { get; set; }
        public int? Quantity { get; set; }
    }
}
