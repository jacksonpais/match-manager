using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Services.Communication.Email.Model;
using MatchManager.Services.Communication.Interface;

namespace MatchManager.Services.Communication.Email.Interface
{
    public interface IEmailService: ICommunicationSerice
    {
        public EmailReceiver Receiver { get; set; }
        public EmailProperties EmailProperties { get; set; }
    }
}
