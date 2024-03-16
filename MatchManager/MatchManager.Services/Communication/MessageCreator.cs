using MatchManager.Domain.Entities.Account;
using MatchManager.Services.Communication.Interface;

namespace MatchManager.Services.Communication
{
    public class MessageCreator
    {
        private IMessageService messageService;

        //Constructor: assigns strategy to interface  
        public MessageCreator(IMessageService _messageService)
        {
            messageService = _messageService;
        }

        //Executes the strategy  
        public string CreateRegistrationVerificationMessage(string name, string url, string mainEmail)
        {
            return messageService.CreateRegistrationVerificationMessage(name, url, mainEmail);
        }
    }
}
