using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Encrypt
{
    public class Hex
    {
        public static string String2Hex(string str, bool space)
        {
            if (space)
                return BitConverter.ToString(Encoding.Default.GetBytes(str)).Replace("-", " ");
            else
                return BitConverter.ToString(Encoding.Default.GetBytes(str)).Replace("-", "");
        }
        public static string Hex2String(string mHex)
        {
            mHex = Regex.Replace(mHex, "[^0-9A-Fa-f]", "");
            if (mHex.Length % 2 != 0)
                mHex = mHex.Remove(mHex.Length - 1, 1);
            if (mHex.Length <= 0) return "";
            byte[] vBytes = new byte[mHex.Length / 2];
            for (int i = 0; i < mHex.Length; i += 2)
                if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                    vBytes[i / 2] = 0;
            return Encoding.Default.GetString(vBytes);
        }
    }
}
