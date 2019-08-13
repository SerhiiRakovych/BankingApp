using System.Security.Cryptography;
using System.Text;

namespace BankingApp.Methods
{
    public static class CustomMethods
    {
        public static string CalculateMD5Hash(string password, string salt)
        {
            string input = salt + password;

            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(input);
            byte[] hash = md5.ComputeHash(inputBytes);

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }
    }
}