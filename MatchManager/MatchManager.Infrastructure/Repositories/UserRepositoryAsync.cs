using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MatchManager.Infrastructure.Repositories.Account.Interface;
using UserManager.Core.Enums;
using UserManager.Data.Context;
using UserManager.Domain.Entities;

namespace UserManager.Infrastructure.Repositories
{
    public class UserRepositoryAsync : IUserRepositoryAsync<UserMaster>
    {
        private readonly UserContext _db;
        public UserRepositoryAsync(UserContext db)
        {
            _db = db;
        }

        public UserMaster GetUser(string username)
        {
            return _db.AppUserMaster.FirstOrDefault(x => x.UserName.ToLower() == username.ToLower());
        }

        public UserMaster GetUser(long userid)
        {
            return _db.AppUserMaster.FirstOrDefault(x => x.UserId == userid);
        }

        public string GetUserSaltbyUserid(long userid)
        {
            throw new NotImplementedException();
        }

        public bool IsUserActivated(long userid)
        {
            throw new NotImplementedException();
        }

        public bool IsUserPresent(string email)
        {
            throw new NotImplementedException();
        }

        public void SaveUser(UserMaster user)
        {
            throw new NotImplementedException();
        }
    }
}
