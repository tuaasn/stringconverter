using System;
using System.Collections.Generic;
using System.Text;

namespace StringConverter.Models
{
    public class MethodEncrypt
    {
        public static MethodEncrypt AesEncoded = new MethodEncrypt(1, "AES Encrypt");
        public static MethodEncrypt AesDecoded = new MethodEncrypt(2, "AES Decrypt");
        public static MethodEncrypt DesEncoded = new MethodEncrypt(3, "DES Encrypt");
        public static MethodEncrypt DesDecoded = new MethodEncrypt(4, "DES Decrypt");
        public static MethodEncrypt HexEncoded = new MethodEncrypt(5, "String to HEX");
        public static MethodEncrypt HexDecoded = new MethodEncrypt(6, "HEX to string");
        public static MethodEncrypt MD5Encoded = new MethodEncrypt(7, "MD5");
        public static MethodEncrypt RsaEncoded = new MethodEncrypt(8, "RSA Encrypt");
        public static MethodEncrypt RsaDecoded = new MethodEncrypt(9, "RSA Decrypt");
        public static MethodEncrypt SHA1Encoded = new MethodEncrypt(10, "SHA1");
        public static MethodEncrypt SHA256Decoded = new MethodEncrypt(11, "SHA256");
        public static MethodEncrypt SHA512Decoded = new MethodEncrypt(12, "SHA512");
        public static MethodEncrypt Base64Encoded = new MethodEncrypt(13, "Base64 Encoded");
        public static MethodEncrypt Base64Decoded = new MethodEncrypt(14, "Base64 Decoded");
        public static MethodEncrypt UnicodeEncoded = new MethodEncrypt(15, "String to Unicode");
        public static MethodEncrypt UnicodeDecoded = new MethodEncrypt(16, "Unicode to String");
        public int Id { get; private set; }
        public string Name { get; private set; }
        protected MethodEncrypt(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static IEnumerable<MethodEncrypt> Methods
        {
            get
            {
                yield return AesEncoded;
                yield return AesDecoded;
                yield return DesEncoded;
                yield return DesDecoded;
                yield return HexEncoded;
                yield return HexDecoded;
                yield return MD5Encoded;
                yield return RsaEncoded;
                yield return RsaDecoded;
                yield return SHA1Encoded;
                yield return SHA256Decoded;
                yield return SHA512Decoded;
                yield return Base64Encoded;
                yield return Base64Decoded;
                yield return UnicodeEncoded;
                yield return UnicodeDecoded;
            }
        }
    }
}
