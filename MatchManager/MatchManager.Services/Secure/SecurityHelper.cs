using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Services.Secure
{
    public static class SecurityHelper
    {
        private const int _expirationMinutes = 1440;

        public static string[] SplitToken(string encryptedText)
        {
            if (encryptedText == null) throw new ArgumentNullException(nameof(encryptedText));
            AESAlgorithm aesAlgorithm = new AESAlgorithm();
            var decryptkey = aesAlgorithm.DecryptFromBase64String(encryptedText);
            string[] parts = decryptkey.Split(new char[] { ':' });
            return parts;
        }

        public static Int16 IsTokenValid(string[] parts, string receivedtoken, string storedtoken)
        {
            if (!string.Equals(receivedtoken, storedtoken, StringComparison.Ordinal))
            {
                return 1;
            }

            long ticks = long.Parse(parts[0]);
            DateTime timeStamp = new DateTime(ticks);
            bool expired = Math.Abs((DateTime.UtcNow - timeStamp).TotalMinutes) > _expirationMinutes;
            if (expired)
            {
                return 2;
            }

            return 0;
        }
    }
}
