using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestRegisterUserJsonBuilder
    {
        public static RequestRegisterUserJson Build(int passwordLength = 10)
        {
            return new Faker<RequestRegisterUserJson>()
                .RuleFor(u => u.FirstName, f => f.Name.FirstName())
                .RuleFor(u => u.LastName, f => f.Name.LastName())
                .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, DateTime.Now.AddYears(-18)))
                .RuleFor(u => u.Email, (f, user) => f.Internet.Email(user.FirstName))
                .RuleFor(u => u.Password, f => f.Internet.Password(passwordLength, true, null, "@1Aa"))
                .Generate();
        }
    }
}