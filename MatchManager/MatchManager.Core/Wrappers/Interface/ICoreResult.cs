namespace MatchManager.Core.Wrappers.Interface
{
    public interface ICoreResult
    {
        public bool IsSuccess { get; set; }
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}
