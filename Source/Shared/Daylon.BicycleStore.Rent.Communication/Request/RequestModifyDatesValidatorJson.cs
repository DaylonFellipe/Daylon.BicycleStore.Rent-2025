namespace Daylon.BicycleStore.Rent.Communication.Request
{
    public class RequestModifyDatesValidatorJson
    {
        // Time Properties
        public DateTime? RentalStart { get; set; }
        public int? RentalDays { get; set; }
        public int? ExtraDays { get; set; }
    }
}
