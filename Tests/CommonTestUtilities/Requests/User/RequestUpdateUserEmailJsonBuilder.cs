using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestUpdateUserEmailJsonBuilder
    {
        public static RequestUpdateUserEmailJson Build(int passwordLength = 10)
        {
            return new Faker<RequestUpdateUserEmailJson>()
                .RuleFor(u => u.Id, f => Guid.NewGuid())
                .RuleFor(u => u.NewEmail, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => f.Internet.Password(passwordLength, true, null, "@1Aa"))
                .Generate();
        }
    }
}
