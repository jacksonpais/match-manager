namespace MatchManager.Services.Communication.Email.Model
{
    public class EmailReceiver
    {
        public required string ToAddress { get; set; }
        public required List<string> Bcc { get; set; }
        public required List<string> Cc { get; set; }
    }
}