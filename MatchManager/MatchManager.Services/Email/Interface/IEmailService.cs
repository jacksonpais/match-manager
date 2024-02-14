using MatchManager.Domain.Entities.Account;
using MatchManager.Domain.Entities.User;
using MatchManager.Services.Email.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Services.Email.Interface
{
    public interface IEmailService
    {
        Task SendEmail(MessageTemplate messageTemplate);
        string CreateRegistrationVerificationEmail(AppUserMaster user, string url, string mainEmail);

        string CreateResetPasswordVerificationEmail(LoginUser user, string url, string mainEmail);
    }
}
