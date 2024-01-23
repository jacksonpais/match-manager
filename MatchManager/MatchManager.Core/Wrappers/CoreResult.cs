using MatchManager.Core.Wrappers.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

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
