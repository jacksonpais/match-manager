using MatchManager.Core.Services.Communication.Interface;
using MatchManager.Services.Communication.Email.Interface;
using MatchManager.Services.Communication.Interface;

namespace MatchManager.Core.Services.Communication.Email
{
    internal class EmailCommunicationServiceAsync : ICommunicationServiceAsync
    {
        private readonly IEmailService _emailService;

        public EmailCommunicationServiceAsync(IEmailService emailService)
        {
            _emailService = emailService;
        }

        public async Task SendAsync(IMessageTemplate messageTemplate)
        {
            await _emailService.Send(messageTemplate);
        }
    }
}
