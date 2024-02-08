namespace MeetApp.Data.Seeds.Interfaces
{
    public interface ISeedData<T> where T : class
    {
        public List<T> AddSeedData();
    }
}
