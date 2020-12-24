using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using Encrypt;

namespace StringConverter.Utility
{
    public class ConvertTool
    {
        public const string IV = "1234567890qwerty";
        public static string Base64(string s, bool c)
        {
            if (c)
            {
                return Convert.ToBase64String(Encoding.UTF8.GetBytes(s));
            }
            else
            {
                try
                {
                    return Encoding.UTF8.GetString(Convert.FromBase64String(s));
                }
                catch
                {
                    return string.Empty;
                }
            }
        }
        #region Comment
        //public static string ChangeEncode(string text, string row, string to)
        //{
        //    byte[] bs = Encoding.GetEncoding(row).GetBytes(text);
        //    return Encoding.GetEncoding(to).GetString(bs);
        //}
        //public static string MD5Encrypt(string password)
        //{
        //    MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();
        //    byte[] hashedDataBytes;
        //    hashedDataBytes = md5Hasher.ComputeHash(Encoding.Default.GetBytes(password));
        //    StringBuilder tmp = new StringBuilder();
        //    foreach (byte i in hashedDataBytes)
        //    {
        //        tmp.Append(i.ToString("x2"));
        //    }
        //    return tmp.ToString();
        //}
        //public static string sha1(string enstr)
        //{
        //    var strRes = Encoding.Default.GetBytes(enstr);
        //    HashAlgorithm iSha = new SHA1CryptoServiceProvider();
        //    strRes = iSha.ComputeHash(strRes);
        //    var enText = new StringBuilder();
        //    foreach (byte iByte in strRes)
        //    {
        //        enText.AppendFormat("{0:x2}", iByte);
        //    }
        //    return enText.ToString();
        //}
        //public static string sha256(string enstr)
        //{
        //    var strRes = Encoding.Default.GetBytes(enstr);
        //    HashAlgorithm iSha = new SHA256CryptoServiceProvider();
        //    strRes = iSha.ComputeHash(strRes);
        //    var enText = new StringBuilder();
        //    foreach (byte iByte in strRes)
        //    {
        //        enText.AppendFormat("{0:x2}", iByte);
        //    }
        //    return enText.ToString();
        //}
        //public static string sha512(string enstr)
        //{
        //    var strRes = Encoding.Default.GetBytes(enstr);
        //    HashAlgorithm iSha = new SHA512CryptoServiceProvider();
        //    strRes = iSha.ComputeHash(strRes);
        //    var enText = new StringBuilder();
        //    foreach (byte iByte in strRes)
        //    {
        //        enText.AppendFormat("{0:x2}", iByte);
        //    }
        //    return enText.ToString();
        //}
        #endregion
        internal static string String2Unicode(string source)
        {
            var bytes = Encoding.Unicode.GetBytes(source);
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < bytes.Length; i += 2)
            {
                stringBuilder.AppendFormat("\\u{0}{1}", bytes[i + 1].ToString("x").PadLeft(2, '0'), bytes[i].ToString("x").PadLeft(2, '0'));
            }
            return stringBuilder.ToString();
        }
        internal static string Unicode2String(string source)
        {
            return new Regex(@"\\u([0-9a-fA-F]{4})", RegexOptions.IgnoreCase | RegexOptions.Compiled).Replace(source, x => Convert.ToChar(Convert.ToUInt16(x.Result("$1"), 16)).ToString());
        }
        public static string ConvertNormal(int functionCode, string sourceText)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(sourceText) || string.IsNullOrWhiteSpace(sourceText)) return result;
            sourceText = sourceText.Trim();
            switch (functionCode)
            {
                case 1:
                    result = Base64(sourceText, false);
                    break;
                case 2:
                    result = Base64(sourceText, true);
                    break;
                case 3:
                    result = Hex.Hex2String(sourceText);
                    break;
                case 4:
                    result = Hex.String2Hex(sourceText, false);
                    break;
                case 5:
                    result = HttpUtility.UrlDecode(sourceText);
                    break;
                case 6:
                    result = HttpUtility.UrlEncode(sourceText);
                    break;
                case 7:
                    result = String2Unicode(sourceText);
                    break;
                case 8:
                    result = Unicode2String(sourceText);
                    break;
                case 15:
                    result = HttpUtility.HtmlDecode(sourceText);
                    break;
                case 16:
                    result = HttpUtility.HtmlEncode(sourceText);
                    break;
                case 17:
                    result = MD5.ComputeMD5Hash(sourceText);
                    break;
                case 18:
                    result = SHA.ComputeSHA1Hash(sourceText);
                    break;
                case 19:
                    result = SHA.ComputeSHA256Hash(sourceText);
                    break;
                case 20:
                    result = SHA.ComputeSHA512Hash(sourceText);
                    break;
                case 21:
                    result = new string(sourceText.Reverse().ToArray());
                    break;
                case 22:
                    result = sourceText.ToUpper();
                    break;
                case 23:
                    result = sourceText.ToLower();
                    break;
                case 24:
                    result = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(sourceText.ToLower());
                    break;
                case 28:
                    if (!Morse.IsInit()) Morse.InitializeDictionary();
                    result = Morse.Decode(sourceText);
                    break;
                case 29:
                    if (!Morse.IsInit()) Morse.InitializeDictionary();
                    result = Morse.Encode(sourceText.ToUpper());
                    break;
                default:
                    break;
            }
            return result;
        }
        public static string ConvertIncludePassword(int functionCode, string sourceText, string password)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(sourceText) || string.IsNullOrWhiteSpace(sourceText)) return result;
            sourceText = sourceText.Trim();
            switch (functionCode)
            {
                case 9:
                    result = AES.Decrypt(sourceText, password, IV);
                    break;
                case 10:
                    result = AES.Encrypt(sourceText, password, IV);
                    break;
                case 11:
                    result = DES.Decrypt(sourceText, password, IV);
                    break;
                case 12:
                    result = DES.Encrypt(sourceText, password, IV);
                    break;
                case 13:
                    result = RSA.Decrypt(sourceText, password);
                    break;
                case 14:
                    result = RSA.Encrypt(sourceText, password);
                    break;
                case 25:
                    result = PasswordEncypt.Decrypt(sourceText, password);
                    break;
                case 26:
                    result = PasswordEncypt.Encrypt(sourceText, password);
                    break;
                default:
                    break;
            }
            return result;
        }

        public static string ConvertAscii(string sourceText)
        {
            TextToBitmap textToBitmap = new TextToBitmap();
            textToBitmap.Input = sourceText;
            var bit = textToBitmap.GetBitmap();
            return bit.ASCIIFilter();
        }
    }
}
