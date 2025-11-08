using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request;

namespace CommonTestUtilities.Requests.RentalOrder
{
    public class RequestModifyDatesValidatorJsonBuilder
    {
        public static RequestModifyDatesValidatorJson Build()
        {
            return new Faker<RequestModifyDatesValidatorJson>()
              .RuleFor(r => r.RentalStart, f => f.Date.Future())
              .RuleFor(r => r.RentalDays, f => f.Random.Int(1, 90))
              .RuleFor(r => r.ExtraDays, f => f.Random.Int(1, 90))
              .Generate();
        }
    }
}
