using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestUpdateUserNameJsonBuilder
    {
        public static RequestUpdateUserNameJson Build(Guid? id = null)
        {
            return new Faker<RequestUpdateUserNameJson>()
                .RuleFor(u => u.Id, f => id ?? Guid.NewGuid())
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .Generate();
        }
    }
}