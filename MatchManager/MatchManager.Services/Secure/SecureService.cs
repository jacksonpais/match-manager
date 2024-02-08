using MatchManager.Services.SecurityService.Interface;
using Microsoft.AspNetCore.DataProtection;

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
