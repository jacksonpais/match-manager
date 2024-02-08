using System.Net;

namespace MatchManager.Core.Wrappers.Interface
{
    public interface IResponse : ICoreResult
    {
        public HttpStatusCode StatusCode { get; set; }
    }
}
