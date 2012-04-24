using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;

namespace StudyingController.Common
{
    public static class HashHelper
    {
        public static string ComputeHash(string inputString)
        {
            string strHash = string.Empty;

            foreach (byte b in new MD5CryptoServiceProvider().ComputeHash(Encoding.Default.GetBytes(inputString)))
            {
                strHash += b.ToString("X2");
            }
            return strHash;
        }
    }
}
