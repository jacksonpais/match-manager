using MatchManager.Services.Communication.Interface;

namespace MatchManager.Services.Communication
{
    public class CommunicateClient
    {
        private ICommunicationSerice communicationService;

        //Constructor: assigns strategy to interface  
        public CommunicateClient(ICommunicationSerice _communicationService)
        {
            communicationService = _communicationService;
        }

        //Executes the strategy  
        public async Task SendAsync()
        {
            await communicationService.SendAsync();
        }
    }
}
