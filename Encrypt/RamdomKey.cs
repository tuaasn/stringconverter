using System;
using System.Security.Cryptography;
using System.Text;

namespace Encrypt
{
    public class RamdomKey
    {
        /// <summary>
        /// Generate a random key
        /// </summary>
        /// <param name="n">key length，IV is 16，Key is 32</param>
        /// <returns>return random value</returns>
        public static string GetRandomStr(int length)
        {
            char[] arrChar = new char[]{
               'a','b','d','c','e','f','g','h','i','j','k','l','m','n','p','r','q','s','t','u','v','w','z','y','x',
               '0','1','2','3','4','5','6','7','8','9',
               'A','B','C','D','E','F','G','H','I','J','K','L','M','N','Q','P','R','T','S','V','U','W','X','Y','Z'
              };

            StringBuilder num = new StringBuilder();

            Random rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < length; i++)
            {
                num.Append(arrChar[rnd.Next(0, arrChar.Length)].ToString());
            }

            return num.ToString();
        }

        public static byte[] CreateAesKey(string key)
        {
            var hash = new SHA512CryptoServiceProvider();
            var aesKey = new byte[32];
            Buffer.BlockCopy(hash.ComputeHash(Encoding.UTF8.GetBytes(key)), 0, aesKey, 0, 32);
            return aesKey;
        }
    }
}
