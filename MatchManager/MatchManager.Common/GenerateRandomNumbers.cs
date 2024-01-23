﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchManager.Common
{
    public class GenerateRandomNumbers
    {
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var random = new SecureRandomNumberGenerator();
            return random.Next(min, max);
        }

        public static string GenerateRandomDigitCode(int length)
        {
            var random = new SecureRandomNumberGenerator();
            var str = string.Empty;
            for (var i = 0; i < length; i++)
                str = string.Concat(str, random.Next(10).ToString());
            return str;
        }
    }
}
