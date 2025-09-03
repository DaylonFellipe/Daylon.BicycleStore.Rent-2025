namespace Daylon.BicycleStore.Rent.Communication.Request.User
{
    public class RequestUpdateUserDateOfBirthJson
    {
        public Guid Id { get; set; }
        public DateTime NewDateOfBirth { get; set; }
        public DateTime OldDateOfBirth { get; set; }
    }
}
