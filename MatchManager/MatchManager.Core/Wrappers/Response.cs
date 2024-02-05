using MatchManager.Core.Wrappers.Interface;
using System.Net;

namespace MatchManager.Core
{
    public class Response : IResponse    
    {
        public Response()
        {
            ErrorMessages = new List<string>();
        }
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
