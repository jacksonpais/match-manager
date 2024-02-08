namespace MatchManager.Services.SecurityService.Interface
{
    public interface ISecureService
    {
        public string Encrypt(string key, string value);

        public string Decrypt(string key, string encrytedText);
    }
}
