using MatchManager.Services.Communication.Interface;

namespace MatchManager.Core.Services.Communication.Interface
{
    public interface ICommunicationServiceAsync
    {
        Task SendAsync(IMessageTemplate messageTemplate);
    }
}
