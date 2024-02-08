using MatchManager.Core.Wrappers.Interface;

namespace MatchManager.Core.Wrappers
{
    public class CoreResult : ICoreResult
    {
        public CoreResult()
        {
            ErrorMessages = new List<string>();
        }
        public bool IsSuccess { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
