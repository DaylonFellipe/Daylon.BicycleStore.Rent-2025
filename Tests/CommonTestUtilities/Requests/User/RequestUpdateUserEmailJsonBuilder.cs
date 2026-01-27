using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestUpdateUserEmailJsonBuilder
    {
        public static RequestUpdateUserEmailJson Build(Guid? id = null, string? password = null, int passwordLength = 10)
        {
            return new Faker<RequestUpdateUserEmailJson>()
                .RuleFor(u => u.Id, f => id ?? Guid.NewGuid())
                .RuleFor(u => u.NewEmail, f => f.Internet.Email())
                .RuleFor(u => u.Password, f => password ?? f.Internet.Password(passwordLength, true, null, "@1Aa"))
                .Generate();
        }
    }
}
