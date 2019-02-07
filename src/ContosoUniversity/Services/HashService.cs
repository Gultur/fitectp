using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Text;

namespace ContosoUniversity.Services
{
    public class HashService
    {
        // Service encodage - Helper?
        // TODO : Move to external service
        public static string GenerateSHA256String(string inputString)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha256.ComputeHash(bytes);
            return BitConverter.ToString(hash);
        }
    }
}