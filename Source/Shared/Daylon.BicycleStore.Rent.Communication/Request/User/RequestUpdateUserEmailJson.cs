namespace Daylon.BicycleStore.Rent.Communication.Request.User
{
    public class RequestUpdateUserEmailJson
    {
        public Guid Id { get; set; }
        public string NewEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
