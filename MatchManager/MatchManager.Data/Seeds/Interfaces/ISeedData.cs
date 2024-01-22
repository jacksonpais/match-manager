using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetApp.Data.Seeds.Interfaces
{
    public interface ISeedData<T> where T : class
    {
        public List<T> AddSeedData();
    }
}
