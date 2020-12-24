//using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StringConverter.Utility
{
    public static class Morse
    {
        private static Dictionary<char, string> _morseAlphabetDictionary;
        private static Dictionary<string, char> _decodeMorseAlphabetDictionary;
        private static bool isInit = false;
        public static void InitializeDictionary()
        {
            _morseAlphabetDictionary = new Dictionary<char, string>()
                {
            // Characters
            { 'A',".-" },
            { 'B',"-..." },
            { 'C',"-.-." },
            { 'D',"-.." },
            { 'E',"." },
            { 'F',"..-." },
            { 'G',"--" },
            { 'H',"...." },
            { 'I',".." },
            { 'J',".---" },
            { 'K',"-.-" },
            { 'L',".-.." },
            { 'M',"--" },
            { 'N',"-." },
            { 'O',"---" },
            { 'P',".--." },
            { 'Q',"--.-" },
            { 'R',".-." },
            { 'S',"..." },
            { 'T',"-" },
            { 'U',"..-" },
            { 'V',"...-" },
            { 'W',".--" },
            { 'X',"-..-" },
            { 'Y',"-.--" },
            { 'Z',"--.." },

            // Numbers
            { '1',".----" },
            { '2',"..---" },
            { '3',"...--" },
            { '4',"....-" },
            { '5',"....." },
            { '6',"-...." },
            { '7',"--..." },
            { '8',"---.." },
            { '9',"----." },
            { '0',"-----" },
           
            // Special Characters
            { '.',".-.-.-" }, // Fullstop
            { ',',"--..--" }, // Comma
            { ':',"---..." }, // Colon
            { '?',"..--.." }, // Question Mark
            { '\'',".----." }, // Apostrophe
            { '-',"-....-" }, // Hyphen, dash, minus
            { '/',"-..-." }, // Slash. division
            { '"',".-..-." }, // Quotaion mark
            { '=',"-...-" }, // Equal sign
            { '+',".-.-." }, // Plus
            //{ '*',"-..-" }, // multiplication
            { '@',".--.-." }, // At the rate of
	        { '&',".-..." },
            { ';',"-.-.-." },
            { '_',"..--.-" },
            { '$',"...-..-" },
            { '!',"-.-.--" },   
            // Brackets
            { '(',"-.--." }, // Left bracket
            { ')',"-.--.-" }, // right bracket           
        };
            _decodeMorseAlphabetDictionary = new Dictionary<string, char>()
                                   {
                                       { ".-",'A'},
                                       { "-...",'B'},
                                       {"-.-.", 'C'},
                                       {"-..", 'D'},
                                       {".", 'E'},
                                       {"..-.",'F' },
                                       {"--.", 'G'},
                                       {"....", 'H'},
                                       {"..", 'I'},
                                       {".---", 'J'},
                                       { "-.-",'K'},
                                       {".-..", 'L'},
                                       {"--", 'M'},
                                       {"-.",'N'},
                                       {"---", 'O'},
                                       {".--.",'P' },
                                       {"--.-",'Q' },
                                       {".-.",'R' },
                                       {"...", 'S'},
                                       { "-",'T'},
                                       { "..-",'U'},
                                       {"...-",'V' },
                                       { ".--", 'W'},
                                       { "-..-",'X'},
                                       {"-.--", 'Y'},
                                       {"--..", 'Z'},
                                       // Numbers
                                       {"-----",'0' },
                                       {".----", '1'},
                                       {"..---",'2' },
                                       { "...--",'3'},
                                       { "....-",'4'},
                                       {".....",'5' },
                                       {"-....", '6'},
                                       {"--...",'7' },
                                       {"---..",'8' },
                                       {"----.",'9' },
                                       // Special Characters
                                        { ".-.-.-",'.' }, // Fullstop
                                        { "--..--",',' }, // Comma
                                        { "---...",':' }, // Colon
                                        { "..--..",'?' }, // Question Mark
                                        { ".----.",'\'' }, // Apostrophe
                                        { "-....-", '-'}, // Hyphen, dash, minus
                                        { "-..-.",'/' }, // Slash. division
                                        { ".-..-.",'"' }, // Quotaion mark
                                        { "-...-",'=' }, // Equal sign
                                        { ".-.-.",'+' }, // Plus
                                        //{ "-..-",'*' }, // multiplication
                                        { ".--.-.",'@' }, // At the rate of
	                                    { ".-...",'&' },
                                        { "-.-.-.",';' },
                                        { "..--.-",'_' },
                                        { "...-..-",'$' },
                                        { "-.-.--",'!' },   
                                        // Brackets
                                        { "-.--.",'(' }, // Left bracket
                                        { "-.--.-",')' }, // right bracket
                                   };
            isInit = true;
        }
        public static string Encode(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char character in input)
            {
                if (_morseAlphabetDictionary.ContainsKey(character))
                {
                    stringBuilder.Append(_morseAlphabetDictionary[character] + " ");
                }
                else if (character == ' ')
                {
                    stringBuilder.Append("/ ");
                }
                else
                {
                    stringBuilder.Append(character + " ");
                }
            }

            return stringBuilder.ToString();
        }
        public static string Decode(string input)
        {
            StringBuilder stringBuilder = new StringBuilder();
            var inputs = input.Split(' ');
            foreach (string character in inputs)
            {
                if (_decodeMorseAlphabetDictionary.ContainsKey(character))
                {
                    stringBuilder.Append(_decodeMorseAlphabetDictionary[character]);
                }
                else if (character == "/")
                {
                    stringBuilder.Append(" ");
                }
                else if (character == " ")
                {
                    stringBuilder.Append(" ");
                }
                else
                {
                    stringBuilder.Append(character);
                }
            }
            return stringBuilder.ToString();
        }
        public static bool IsInit()
        {
            return isInit;
        }
        private static bool IsValidMorse(string sentence)
        {
            var countDot = sentence.Count(x => x == '.');
            var countDash = sentence.Count(x => x == '-');
            var countSpace = sentence.Count(x => x == ' ');

            return
                sentence.Length > (countDot + countDash + countSpace)
                ? false : true;
        }
    }

    #region For Feature
    //public static class MorseAudio
    //{
    //    private static byte[] GetWavBytes(string filename)
    //    {
    //        var bytes = File.ReadAllBytes(filename);
    //        var headerLength = GetWavHeaderLength(bytes);
    //        return bytes.Skip(headerLength + 16 * 4).ToArray();
    //    }

    //    private static int GetWavHeaderLength(byte[] wave)
    //    {
    //        int subchunk1Size = BitConverter.ToInt32(wave, 16);
    //        if (subchunk1Size < 16)
    //        {
    //            return -1;
    //        }
    //        return subchunk1Size;
    //    }

    //    private static int[] GetSamples(byte[] wave)
    //    {
    //        var res = new int[wave.Length / 4];
    //        for (int i = 0; i < wave.Length - 4; i += 4)
    //        {
    //            res[i / 4] = BitConverter.ToInt32(wave, i);
    //        }
    //        return res;
    //    }

    //    private static byte[] Minimize(int[] samples)
    //    {
    //        int frameLength = 16;
    //        var res = new byte[samples.Length / frameLength];
    //        long offset = 0;
    //        int resIndex = 0;
    //        while (offset <= samples.Length - frameLength)
    //        {
    //            byte isBeep = 0;
    //            for (int i = 0; i < frameLength; i++)
    //            {
    //                if (samples[offset + i] != 0)
    //                {
    //                    isBeep = 1;
    //                    break;
    //                }
    //            }
    //            res[resIndex] = isBeep;
    //            resIndex++;
    //            offset += frameLength;
    //        }

    //        return res;
    //    }

    //    private static string GetMorseString(byte[] beeps)
    //    {
    //        StringBuilder morseSb = new StringBuilder();
    //        const int DOT_LENGTH_MAX = 200;
    //        const byte DOT_LENGTH_MIN = 20;
    //        const int DASH_LENGTH_MIN = 300;
    //        const int PAUSE_LENGTH_MIN = 1000;
    //        const int SPACE_LENGTH_MIN = 3000;

    //        bool countingBeepLength = false;
    //        int beepLength = 0;
    //        bool countingPauseLength = false;
    //        int pauseLength = 0;
    //        for (int i = 0; i < beeps.Length; i++)
    //        {

    //            if (beeps[i] == 1)
    //            {
    //                if (countingPauseLength)
    //                {
    //                    if (pauseLength > SPACE_LENGTH_MIN)
    //                    {
    //                        morseSb.Append('\\');
    //                    }
    //                    else if (pauseLength > PAUSE_LENGTH_MIN)
    //                    {
    //                        morseSb.Append(' ');
    //                    }
    //                    countingPauseLength = false;
    //                    pauseLength = 0;
    //                }
    //                beepLength++;
    //                // signal start
    //                countingBeepLength = true;
    //            }
    //            else
    //            {
    //                countingPauseLength = true;
    //                pauseLength++;
    //                if (!countingBeepLength)
    //                    continue;
    //                // signal ended
    //                if (beepLength < DOT_LENGTH_MAX && beepLength > DOT_LENGTH_MIN)
    //                {
    //                    morseSb.Append('.');
    //                }
    //                else if (beepLength > DASH_LENGTH_MIN)
    //                {
    //                    morseSb.Append('-');
    //                }
    //                countingBeepLength = false;
    //                beepLength = 0;
    //            }
    //        }

    //        return morseSb.ToString();
    //    }

    //    private static string ConvertMp3ToWav(string mp3path)
    //    {
    //        string fileName = Path.GetFileNameWithoutExtension(mp3path);
    //        string wavPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), $"{fileName}.wav");
    //        using (Mp3FileReader mp3 = new Mp3FileReader(mp3path))
    //        {
    //            using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
    //            {
    //                WaveFileWriter.CreateWaveFile(wavPath, pcm);
    //            }
    //        }
    //        return wavPath;
    //    }

    //    public static string Convert(FileResult file)
    //    {
    //        if (file == null) return string.Empty;
    //        bool fileGenerated = false;
    //        string wavPath;
    //        if (file.FileName.EndsWith(".wav"))
    //        {
    //            wavPath = file.FullPath;
    //        }
    //        else
    //        {
    //            wavPath = ConvertMp3ToWav(file.FullPath);
    //            fileGenerated = true;
    //        }
    //        var bytes = GetWavBytes(wavPath);
    //        if (fileGenerated && wavPath.EndsWith(".wav"))
    //        {
    //            File.Delete(wavPath);
    //        }
    //        var samples = GetSamples(bytes);
    //        var beeps = Minimize(samples);
    //        var morseString = GetMorseString(beeps);
    //        if (!Morse.IsInit())
    //        {
    //            Morse.InitializeDictionary();
    //        }
    //        var text = Morse.Decode(morseString);
    //        return text;
    //    }
    //}
    //public class TextToMorse
    //{
    //    // Character speed in WPM
    //    public int CharacterSpeed { get; set; }
    //    // Overall speed in WPM (must be <= character speed)
    //    public int WordSpeed { get; set; }
    //    // Tone frequency
    //    public double Frequency { get; set; }

    //    public TextToMorse(int charSpeed, int wordSpeed, double frequency)
    //    {
    //        CharacterSpeed = charSpeed;
    //        WordSpeed = wordSpeed;
    //        Frequency = frequency;
    //    }

    //    public TextToMorse(int charSpeed, int wordSpeed) : this(charSpeed, wordSpeed, 600.0) { }
    //    public TextToMorse(int wpm) : this(wpm, wpm) { }
    //    public TextToMorse() : this(20) { }

    //    // Return given number of seconds of sine wave
    //    private short[] GetWave(double seconds)
    //    {
    //        short[] waveArray;
    //        int samples = (int)(11025 * seconds);

    //        waveArray = new short[samples];

    //        for (int i = 0; i < samples; i++)
    //        {
    //            waveArray[i] = Convert.ToInt16(32760 * Math.Sin(i * 2 * Math.PI * Frequency / 11025));
    //        }

    //        return waveArray;
    //    }

    //    // Return given number of seconds of flatline. This could also be
    //    // achieved with slnt chunks inside a wavl chunk, but the resulting
    //    // file might not be universally readable. If saving space is that
    //    // important, it would be better to compress the output as mp3 or ogg
    //    // anyway.
    //    private short[] GetSilence(double seconds)
    //    {
    //        short[] waveArray;
    //        int samples = (int)(11025 * seconds);

    //        waveArray = new short[samples];

    //        return waveArray;
    //    }

    //    // Dot -- 1 unit long
    //    private short[] GetDot()
    //    {
    //        return GetWave(1.2 / CharacterSpeed);
    //    }

    //    // Dash -- 3 units long
    //    private short[] GetDash()
    //    {
    //        return GetWave(3.6 / CharacterSpeed);
    //    }

    //    // Inter-element space -- 1 unit long
    //    private short[] GetInterEltSpace()
    //    {
    //        return GetSilence(1.2 / CharacterSpeed);
    //    }

    //    // Space between letters -- nominally 3 units, but adjusted for
    //    // Farnsworth timing (if word speed is lower than character
    //    // speed) based on ARRL's Morse code timing standard:
    //    // http://www.arrl.org/files/file/Technology/x9004008.pdf
    //    private short[] GetInterCharSpace()
    //    {
    //        double delay = (60.0 / WordSpeed) - (32.0 / CharacterSpeed);
    //        double spaceLength = 3 * delay / 19;
    //        return GetSilence(spaceLength);
    //    }

    //    // Space between words -- nominally 7 units, but adjusted for
    //    // Farnsworth timing in case word speed is lower than character
    //    // speed.
    //    private short[] GetInterWordSpace()
    //    {
    //        double delay = (60.0 / WordSpeed) - (32.0 / CharacterSpeed);
    //        double spaceLength = 7 * delay / 19;
    //        return GetSilence(spaceLength);
    //    }

    //    // Return a single character as a waveform
    //    private short[] GetCharacter(string character)
    //    {
    //        short[] space = GetInterEltSpace();
    //        short[] dot = GetDot();
    //        short[] dash = GetDash();
    //        List<short> morseChar = new List<short>();

    //        string morseSymbol = Characters.Symbols[character];
    //        for (int i = 0; i < morseSymbol.Length; i++)
    //        {
    //            if (i > 0)
    //                morseChar.AddRange(space);
    //            if (morseSymbol[i] == '-')
    //                morseChar.AddRange(dash);
    //            else if (morseSymbol[i] == '.')
    //                morseChar.AddRange(dot);
    //        }

    //        return morseChar.ToArray<short>();
    //    }

    //    // Return a word as a waveform
    //    private short[] GetWord(string word)
    //    {
    //        List<short> data = new List<short>();

    //        for (int i = 0; i < word.Length; i++)
    //        {
    //            if (i > 0)
    //                data.AddRange(GetInterCharSpace());
    //            if (word[i] == '<')
    //            {
    //                // Prosign
    //                int end = word.IndexOf('>', i);
    //                if (end < 0)
    //                    throw new ArgumentException();
    //                data.AddRange(GetCharacter(word.Substring(i, end + 1 - i)));
    //                i = end;
    //            }
    //            else
    //            {
    //                data.AddRange(GetCharacter(word[i].ToString()));
    //            }
    //        }

    //        return data.ToArray<short>();
    //    }

    //    // Return a string (lower case text only, unrecognized characters
    //    // throw an exception -- see Characters.cs for the list of recognized
    //    // characters) as a waveform wrapped in a DataChunk, ready to by added
    //    // to a wave file.
    //    private DataChunk GetText(string text)
    //    {
    //        List<short> data = new List<short>();

    //        string[] words = text.Split(' ');

    //        for (int i = 0; i < words.Length; i++)
    //        {
    //            if (i > 0)
    //                data.AddRange(GetInterWordSpace());
    //            data.AddRange(GetWord(words[i]));
    //        }

    //        // Pad the end with a little bit of silence. Otherwise the last
    //        // character may sound funny in some media players.
    //        data.AddRange(GetInterCharSpace());

    //        DataChunk dataChunk = new DataChunk(data.ToArray<short>());

    //        return dataChunk;
    //    }

    //    // Returns a byte array in the Wave file format containing the given
    //    // text in morse code
    //    public byte[] ConvertToMorse(string text)
    //    {
    //        DataChunk data = GetText(text.ToLower());
    //        FormatChunk formatChunk = new FormatChunk();
    //        HeaderChunk headerChunk = new HeaderChunk(formatChunk, data);
    //        return headerChunk.ToBytes();
    //    }
    //}
    //static class Characters
    //{
    //    public static Dictionary<string, string> Symbols = new Dictionary<string, string>()
    //    {
    //        { "a", ".-" },
    //        { "b", "-..." },
    //        { "c", "-.-." },
    //        { "d", "-.." },
    //        { "e", "." },
    //        { "f", "..-." },
    //        { "g", "--." },
    //        { "h", "...." },
    //        { "i", ".." },
    //        { "j", ".---" },
    //        { "k", "-.-" },
    //        { "l", ".-.." },
    //        { "m", "--" },
    //        { "n", "-." },
    //        { "o", "---" },
    //        { "p", ".--." },
    //        { "q", "--.-" },
    //        { "r", ".-." },
    //        { "s", "..." },
    //        { "t", "-" },
    //        { "u", "..-" },
    //        { "v", "...-" },
    //        { "w", ".--" },
    //        { "x", "-..-" },
    //        { "y", "-.--" },
    //        { "z", "--.." },
    //        { "0", "-----" },
    //        { "1", ".----" },
    //        { "2", "..---" },
    //        { "3", "...--" },
    //        { "4", "....-" },
    //        { "5", "....." },
    //        { "6", "-...." },
    //        { "7", "--..." },
    //        { "8", "---.." },
    //        { "9", "----." },
    //        { ".", ".-.-.-" },
    //        { ",", "--..--" },
    //        { "?", "..--.." },
    //        { "'", ".----." },
    //        { "/", "-..-." },
    //        { "(", "-.--." },
    //        { ")", "-.--.-" },
    //        { ":", "---..." },
    //        { ";", "-.-.-." },
    //        { "=", "-...-" },
    //        { "+", ".-.-." },
    //        { "-", "-....-" },
    //        { "\"", ".-..-." },
    //        { "@", ".--.-." },
    //        // Prosigns -- see
    //        // http://en.wikipedia.org/wiki/Prosigns_for_Morse_code
    //        { "<aa>", ".-.-" },         // Space down one line (new line)
    //        { "<ar>", ".-.-." },        // Stop copying (end of message)
    //        { "<as>", ".-..." },        // Stand by
    //        { "<bk>", "-...-.-" },      // Break
    //        { "<bt>", "-...-" },        // Space down two lines
    //        { "<cl>", "-.-..-.." },     // Clear (going off the air)
    //        { "<ct>", "-.-.-" },        // Start copying
    //        { "<kn>", "-.--." },        // Go only
    //        { "<sk>", "...-.-" },       // Silent key (going off the air)
    //        { "<sn>", "...-." }         // Understood
    //    };
    //}
    //class DataChunk : WaveChunk
    //{
    //    public short[] ChunkData { get; set; }

    //    public DataChunk(short[] data)
    //    {
    //        ChunkId = "data".ToCharArray();
    //        ChunkSize = (uint)(data.Length * 2);
    //        ChunkData = data;
    //    }

    //    public override byte[] ToBytes()
    //    {
    //        List<byte> bytes = new List<byte>();

    //        bytes.AddRange(Encoding.UTF8.GetBytes(ChunkId));
    //        bytes.AddRange(BitConverter.GetBytes(ChunkSize));

    //        foreach (short datum in ChunkData)
    //            bytes.AddRange(BitConverter.GetBytes(datum));

    //        return bytes.ToArray<byte>();
    //    }
    //}
    //abstract class WaveChunk
    //{
    //    public char[] ChunkId { get; set; }
    //    public uint ChunkSize { get; set; }

    //    public abstract byte[] ToBytes();
    //}
    //class FormatChunk : WaveChunk
    //{
    //    public short CompressionCode { get; set; }
    //    public short NumChannels { get; set; }
    //    public uint SampleRate { get; set; }
    //    public uint BytesPerSecond { get; set; }
    //    public short BlockAlign { get; set; }
    //    public short SignificantBits { get; set; }

    //    public FormatChunk()
    //    {
    //        ChunkId = "fmt ".ToCharArray();
    //        ChunkSize = 16;
    //        CompressionCode = 1;
    //        NumChannels = 1;
    //        SampleRate = 11025;
    //        BytesPerSecond = 22050;
    //        BlockAlign = 2;
    //        SignificantBits = 16;
    //    }

    //    public override byte[] ToBytes()
    //    {
    //        List<byte> bytes = new List<byte>();

    //        bytes.AddRange(Encoding.UTF8.GetBytes(ChunkId));
    //        bytes.AddRange(BitConverter.GetBytes(ChunkSize));
    //        bytes.AddRange(BitConverter.GetBytes(CompressionCode));
    //        bytes.AddRange(BitConverter.GetBytes(NumChannels));
    //        bytes.AddRange(BitConverter.GetBytes(SampleRate));
    //        bytes.AddRange(BitConverter.GetBytes(BytesPerSecond));
    //        bytes.AddRange(BitConverter.GetBytes(BlockAlign));
    //        bytes.AddRange(BitConverter.GetBytes(SignificantBits));

    //        return bytes.ToArray<byte>();
    //    }
    //}
    //class HeaderChunk : WaveChunk
    //{
    //    public char[] RiffType { get; set; }
    //    public FormatChunk FormatChunk { get; set; }
    //    public DataChunk DataChunk { get; set; }

    //    public HeaderChunk(FormatChunk formatChunk, DataChunk dataChunk)
    //    {
    //        ChunkId = "RIFF".ToCharArray();
    //        RiffType = "WAVE".ToCharArray();
    //        FormatChunk = formatChunk;
    //        DataChunk = dataChunk;
    //        ChunkSize = 36 + DataChunk.ChunkSize;
    //    }

    //    public override byte[] ToBytes()
    //    {
    //        List<byte> bytes = new List<byte>();

    //        bytes.AddRange(Encoding.UTF8.GetBytes(ChunkId));
    //        bytes.AddRange(BitConverter.GetBytes(ChunkSize));
    //        bytes.AddRange(Encoding.UTF8.GetBytes(RiffType));
    //        bytes.AddRange(FormatChunk.ToBytes());
    //        bytes.AddRange(DataChunk.ToBytes());

    //        return bytes.ToArray<byte>();
    //    }
    //}
    #endregion
}
