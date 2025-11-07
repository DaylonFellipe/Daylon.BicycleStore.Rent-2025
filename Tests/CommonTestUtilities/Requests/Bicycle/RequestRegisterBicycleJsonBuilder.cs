using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.Bibycle;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace CommonTestUtilities.Requests.Bicycle
{
    public class RequestRegisterBicycleJsonBuilder
    {
        public static RequestRegisterBicycleJson Build()
        {
            return new Faker<RequestRegisterBicycleJson>()
                .RuleFor(b => b.Name, f => f.Commerce.ProductName())
                .RuleFor(b => b.Description, f => f.Commerce.ProductDescription())
                .RuleFor(b => b.Model, f => f.PickRandom<ModelEnum>())
                .RuleFor(b => b.Brand, f => f.PickRandom<BrandEnum>())
                .RuleFor(b => b.Color, f => f.PickRandom<ColorEnum>())
                .RuleFor(b => b.Price, f => double.Parse(f.Commerce.Price(100, 5000)))
                .RuleFor(b => b.Quantity, f => f.Random.Int(0, 100))
                .RuleFor(b => b.DailyRate, f => double.Parse(f.Commerce.Price(10, 500)))
                .Generate();
        }
    }
}