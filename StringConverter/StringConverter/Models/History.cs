using SQLite;
using System;
using StringConverter.Resources;

namespace StringConverter.Models
{
    public class History
    {
        [PrimaryKey]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string SrcText { get; set; }
        public string DesText { get; set; }
        public string Password { get; set; }
        public int Method { get; set; }
        public string MethodName 
        {
            get
            {
                string name = string.Empty;
                switch (Method)
                {
                    case 1:
                        name = Resource.Base64Decoder;
                        break;
                    case 2:
                        name = Resource.Base64Encoder;
                        break;
                    case 3:
                        name = Resource.HexDecoder;
                        break;
                    case 4:
                        name = Resource.HexEncoder;
                        break;
                    case 5:
                        name = Resource.UrlDecoder;
                        break;
                    case 6:
                        name = Resource.UrlEncoder;
                        break;
                    case 7:
                        name = Resource.UnicodeEncoder;
                        break;
                    case 8:
                        name = Resource.UnicodeDecoder;
                        break;
                    case 15:
                        name = Resource.HtmlDecoder;
                        break;
                    case 16:
                        name = Resource.HtmlEncoder;
                        break;
                    case 17:
                        name = Resource.Base64Encoder;
                        break;
                    case 18:
                        name = Resource.Base64Encoder;
                        break;
                    case 19:
                        name = Resource.Base64Encoder;
                        break;
                    case 20:
                        name = Resource.Base64Encoder;
                        break;
                    case 21:
                        name = Resource.ReverseString;
                        break;
                    case 22:
                        name = Resource.UpperCaseString;
                        break;
                    case 23:
                        name = Resource.LowerCaseString;
                        break;
                    case 24:
                        name = Resource.TitleCaseString;
                        break;
                    case 9:
                        name = Resource.AESDecoder;
                        break;
                    case 10:
                        name = Resource.AESEncoder;
                        break;
                    case 11:
                        name = Resource.DESDecoder;
                        break;
                    case 12:
                        name = Resource.DESEncoder;
                        break;
                    case 13:
                        name = Resource.RsaDecoded;
                        break;
                    case 14:
                        name = Resource.RsaEncoded;
                        break;
                    case 25:
                        name = Resource.DecoderCustomPassword;
                        break;
                    case 26:
                        name = Resource.EncoderCustomPassword;
                        break;
                    case 28:
                        name = Resource.MorseDecoder;
                        break;
                    case 29:
                        name = Resource.MorseEncoder;
                        break;
                    case 30:
                        name = Resource.TextToBinary;
                        break;
                    case 31:
                        name = Resource.BinaryToText;
                        break;
                }
                return name;
            }
        }
    }
}
