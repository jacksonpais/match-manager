using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Services.SecurityService.Interface
{
    public interface ISecureService
    {
        public string Encrypt(string key, string value);

        public string Decrypt(string key, string encrytedText);
    }
}
