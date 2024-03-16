using MatchManager.Services.Communication.Interface;

namespace MatchManager.Services.Communication
{
    public class CommunicationClient
    {
        private ICommunicationSerice communicationService;

        //Constructor: assigns strategy to interface  
        public CommunicationClient(ICommunicationSerice _communicationService)
        {
            communicationService = _communicationService;
        }

        //Executes the strategy  
        public async Task<bool> SendAsync()
        {
            return await communicationService.SendAsync();
        }
    }
}
