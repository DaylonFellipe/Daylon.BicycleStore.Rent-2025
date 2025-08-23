namespace Daylon.BicycleStore.Rent.Domain.Entity
{
    public class Person
    {
        // Person Properties
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public int Age { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
