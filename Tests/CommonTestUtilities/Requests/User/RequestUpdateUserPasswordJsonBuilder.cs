using Bogus;
using Daylon.BicycleStore.Rent.Communication.Request.User;

namespace CommonTestUtilities.Requests.User
{
    public class RequestUpdateUserPasswordJsonBuilder
    {
        public static RequestUpdateUserPasswordJson Build(
            Guid? id = null,
            string? oldPassword = null,
            int passwordLength = 10)
        {
            return new Faker<RequestUpdateUserPasswordJson>()
                .RuleFor(u => u.Id, f => id ?? Guid.NewGuid())
                .RuleFor(u => u.NewPassword, f => f.Internet.Password(passwordLength, true, null, "@1Aa"))
                .RuleFor(u => u.OldPassword, f => oldPassword ?? f.Internet.Password(passwordLength, true, null, "@1Aa"))
                .Generate();
        }
    }
}