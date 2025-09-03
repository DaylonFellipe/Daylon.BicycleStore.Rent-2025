namespace Daylon.BicycleStore.Rent.Communication.Request.User
{
    public class RequestUpdateUserPasswordJson
    {
        public Guid Id { get; set; }
        public string OldPassword { get; set; } = string.Empty;
        public string NewPassword { get; set; } = string.Empty;
    }
}
