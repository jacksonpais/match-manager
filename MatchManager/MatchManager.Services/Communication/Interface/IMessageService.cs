using MatchManager.Domain.Entities.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Services.Communication.Interface
{
    public interface IMessageService
    {
        string CreateRegistrationVerificationMessage(string name, string url, string mainEmail);
    }
}
