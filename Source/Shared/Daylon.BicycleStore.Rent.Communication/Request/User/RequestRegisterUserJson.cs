namespace Daylon.BicycleStore.Rent.Communication.Request.User
{
    public class RequestRegisterUserJson
    {
        // User Properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
