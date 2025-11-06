using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestUpdateUserDateOfBirthJsonBuilder
    {
        public static RequestUpdateUserDateOfBirthJson Build()
        {
            return new Faker<RequestUpdateUserDateOfBirthJson>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.NewDateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .Generate();
        }
    }
}
