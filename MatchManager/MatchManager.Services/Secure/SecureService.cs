using MatchManager.Services.SecurityService.Interface;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Services.SecurityService
{
    public class SecureService : ISecureService
    {
        private readonly IDataProtectionProvider _dataProtectionProvider;
        public SecureService(IDataProtectionProvider dataProtectionProvider)
        {
            _dataProtectionProvider = dataProtectionProvider;
        }

        public string Encrypt(string key, string value)
        {
            var protector = _dataProtectionProvider.CreateProtector(key);
            return protector.Protect(value);
        }

        public string Decrypt(string key, string encrytedText)
        {
            var protector = _dataProtectionProvider.CreateProtector(key);
            return protector.Unprotect(encrytedText);
        }
    }
}
