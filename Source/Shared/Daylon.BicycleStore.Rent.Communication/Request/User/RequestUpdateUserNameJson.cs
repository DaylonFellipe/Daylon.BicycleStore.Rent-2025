namespace Daylon.BicycleStore.Rent.Communication.Request.User
{
    public class RequestUpdateUserNameJson
    {
        public Guid Id { get; set; }
        public string? FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; } = string.Empty;
    }
}
