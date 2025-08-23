namespace Daylon.BicycleStore.Rent.Domain.Entity
{
    public class User : Person
    {
        // User Properties
        public Guid Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public DateTime CreatedOn { get; set; }
        public bool Active { get; set; }
    }
}
