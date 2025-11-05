using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestUpdateUserNameJsonBuilder
    {
        public static RequestUpdateUserNameJson Build()
        {
            return new Faker<RequestUpdateUserNameJson>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .Generate();
        }
    }
}