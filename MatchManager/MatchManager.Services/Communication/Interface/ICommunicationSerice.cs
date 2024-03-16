namespace MatchManager.Services.Communication.Interface
{
    public interface ICommunicationSerice
    {
        public string Subject { get; set; }
        public string Message { get; set; }
        Task<bool> SendAsync();
    }
}
