using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Business.Helpers
{
    public static class PaswordHash
    {
        private static string GetMd5(string password) {
            var hashPassword = string.Empty;
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }

                hashPassword = sb.ToString();
            }

            return hashPassword;
        }

        public static bool VerifyPassword(string password, string hashPassword) {
            return GetMd5(password).Equals(hashPassword);
        }
    }
}
