namespace Daylon.BicycleStore.Rent.Communication.Responses
{
    public class ResponseErrorJson
    {
        public IList<string> Errors { get; set; }

        public ResponseErrorJson(string message) => Errors = new List<string> { message };

        public ResponseErrorJson(IEnumerable<string> messages) => Errors = messages.ToList();

    }
}
