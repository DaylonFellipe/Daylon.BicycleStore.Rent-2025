using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request;
using Daylon.BicycleStore.Rent.Domain.Entity.Enum;

namespace CommonTestUtilities.Requests.RentalOrder
{
    public class RequestRegisterRentalOrderJsonBuilder
    {
        public static RequestRegisterRentalOrderJson Build()
        {
            return new Faker<RequestRegisterRentalOrderJson>()
                .RuleFor(r => r.RentalDays, f => f.Random.Int(1, 90))
                .RuleFor(p => p.PaymentMethod, f => f.PickRandom<PaymentMethodEnum>())
                .RuleFor(u => u.BicycleId, f => Guid.NewGuid())
                .Generate();
        }
    }
}
