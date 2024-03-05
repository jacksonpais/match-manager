namespace MatchManager.Domain.Config
{
    public partial class AppConfig
    {
        public required string AppName { get; set; }
        public required string TokenKey { get; set; }
        public required Urls Urls { get; set; }
        public required EmailConfiguration EmailConfiguration { get; set; }
    }

    public partial class EmailConfiguration
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string MailHost { get; set; }
        public int MailPort { get; set; }
    }

    public partial class Urls
    {
        public required Uri DomainUrl { get; set; }
        public required Uri ApiUrl { get; set; }
        public required string RegistrationVerificationUrl { get; set; }
    }
}