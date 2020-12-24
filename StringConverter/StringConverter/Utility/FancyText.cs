using System;
using System.Linq;

namespace StringConverter.Utility
{
    //https://github.com/AzizStark/Quiva
    public class FancyText
    {
        private string[] kaomojiJoy = { " (* ^ ω ^)", " (o^▽^o)", " (≧◡≦)", " ☆⌒ヽ(*\"､^*)chu", " ( ˘⌣˘)♡(˘⌣˘ )", " xD" };
        private string[] kaomojiEmbarassed = { " (⁄ ⁄>⁄ ▽ ⁄<⁄ ⁄)..", " (*^.^*)..,", "..,", ",,,", "... ", ".. ", " mmm..", "O.o" };
        private string[] kaomojiConfuse = { " (o_O)?", " (°ロ°) !?", " (ーー;)?", " owo?" };
        private string[] kaomojiSparkles = { " *:･ﾟ✧*:･ﾟ✧ ", " ☆*:・ﾟ ", "〜☆ ", " uguu.., ", "-.-" };
        public string convertToUwU(String text)
        {
            var textArray = text.Split(' ');
            return textArray.Aggregate<string, string>(null, (current, word) => current + uwuWord(word));
        }

        private string uwuWord(String word)
        {
            string uwu = "";
            char lastChar = word[word.Length - 1];
            string end = null;

            if (lastChar == '.' || lastChar == '?' || lastChar == '!' || lastChar == ',')
            {
                word = word.Remove(word.Length - 1);
                end = lastChar.ToString();

                switch (end)
                {
                    case ".":
                        {
                            if (new Random().Next(3) == 0)
                            {
                                end = kaomojiJoy[new Random().Next(kaomojiJoy.Length)];
                            }

                            break;
                        }
                    case "?":
                        {
                            if (new Random().Next(2) == 0)
                            {
                                end = kaomojiConfuse[new Random().Next(kaomojiConfuse.Length)];
                            }

                            break;
                        }
                    case "!":
                        {
                            if (new Random().Next(2) == 0)
                            {
                                end = kaomojiJoy[new Random().Next(kaomojiJoy.Length)];
                            }

                            break;
                        }
                    case ",":
                        {
                            if (new Random().Next(3) == 0)
                            {
                                end = kaomojiEmbarassed[new Random().Next(kaomojiEmbarassed.Length)];
                            }

                            break;
                        }
                    default:
                        {
                            if (new Random().Next(4) == 0)
                            {
                                end = kaomojiSparkles[new Random().Next(kaomojiSparkles.Length)];
                            }

                            break;
                        }
                }
            }

            if (word.IndexOf('1') > -1)
            {
                if (word.Substring(word.Length - 2) == "le" || word.Substring(word.Length - 2) == "ll")
                {
                    uwu += word.Substring(0, word.Length - 2).Replace('l', 'w').Replace('r', 'w') + word.Substring(word.Length - 2) + end + ' ';
                }
                else if (word.Substring(word.Length - 3) == "les" || word.Substring(word.Length - 3) == "lls")
                {
                    uwu += word.Substring(0, word.Length - 3).Replace('l', 'w').Replace('r', 'w') + word.Substring(word.Length - 3) + end + ' ';
                }
                else
                {
                    uwu += word.Replace('l', 'w').Replace('r', 'w') + end + ' ';
                }
            }
            else if (word.IndexOf('r') > -1)
            {
                if (word.Substring(word.Length - 2) == "er" || word.Substring(word.Length - 2) == "re")
                {
                    uwu += word.Substring(0, word.Length - 2).Replace('r', 'w') + word.Substring(word.Length - 2) + end + ' ';
                }
                else if (word.Substring(word.Length - 3) == "ers" || word.Substring(word.Length - 3) == "res")
                {
                    uwu += word.Substring(0, word.Length - 3).Replace('r', 'w') + word.Substring(word.Length - 3) + end + ' ';
                }
                else
                {
                    uwu += word.Replace('r', 'w') + end + ' ';
                }
            }
            else
            {
                uwu += word + end + ' ';
            }

            uwu = uwu.Replace("you're", "ur");
            uwu = uwu.Replace("youre", "ur");
            uwu = uwu.Replace("fuck", "fwickk");
            uwu = uwu.Replace("shit", "poopoo");
            uwu = uwu.Replace("bitch", "meanie");
            uwu = uwu.Replace("asshole", "b-butthole");
            uwu = uwu.Replace("dick", "peenie");
            uwu = uwu.Replace("penis", "peenie");
            uwu = uwu.Replace("cum", "cummies");
            uwu = uwu.Replace("semen", " cummies ");
            uwu = uwu.Replace("ass", " boi pussy ");
            uwu = uwu.Replace("dad", "daddy");
            uwu = uwu.Replace("father", "daddy");

            //if (uwu.Length > 2 && Char.IsLetter(uwu[0]))
            //{
            //    random = new Random().Next(6);
            //    if (random == 0)
            //    {
            //        uwu = uwu[0] + '-' + uwu;
            //    }
            //}

            return uwu;
        }
    }
}
