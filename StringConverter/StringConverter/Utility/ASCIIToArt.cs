using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace StringConverter.Utility
{
    public class TextToBitmap
    {
        public string Input = "";
        public Font InputFont = new Font("Arial", 20);

        public Color BmpForeground = Color.Black;
        public Color BmpBackground = Color.White;

        private Image bmpText;
        public TextToBitmap() { }
        public TextToBitmap(string input, Font inputFont, Color bmpForeground, Color bmpBackground)
        {
            this.Input = input;
            this.InputFont = inputFont;
            this.BmpForeground = bmpForeground;
            this.BmpBackground = bmpBackground;
        }
        public Bitmap GetBitmap()
        {
            bmpText = new Bitmap(1, 1);
            try
            {
                Graphics g = Graphics.FromImage(bmpText);
                SizeF inputSize = g.MeasureString(Input, InputFont);

                bmpText = new Bitmap(Convert.ToInt32(inputSize.Width),
                    Convert.ToInt32(inputSize.Height));
                g = Graphics.FromImage(bmpText);
                g.FillRectangle(new Pen(BmpBackground).Brush,
                    new Rectangle(0, 0, Convert.ToInt32(inputSize.Width), Convert.ToInt32(inputSize.Height)));
                g.DrawString(Input, InputFont, new Pen(BmpForeground).Brush, new PointF(0, 0));
            }
            catch
            {
                Graphics.FromImage(bmpText).FillRectangle(new Pen(BmpBackground).Brush,
                    new Rectangle(0, 0, 1, 1));
            }
            return (Bitmap)bmpText;
        }
        public override string ToString()
        {
            return Input;
        }
    }
    public class LockBitmap
    {
        Bitmap source = null;
        IntPtr Iptr = IntPtr.Zero;
        BitmapData bitmapData = null;

        public byte[] Pixels { get; set; }
        public int Depth { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }

        public LockBitmap(Bitmap source)
        {
            this.source = source;
        }

        /// <summary>
        /// Lock bitmap data
        /// </summary>
        public void LockBits()
        {
            try
            {
                // Get width and height of bitmap
                Width = source.Width;
                Height = source.Height;

                // get total locked pixels count
                int PixelCount = Width * Height;

                // Create rectangle to lock
                Rectangle rect = new Rectangle(0, 0, Width, Height);

                // get source bitmap pixel format size
                Depth = System.Drawing.Bitmap.GetPixelFormatSize(source.PixelFormat);

                // Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (Depth != 8 && Depth != 24 && Depth != 32)
                {
                    throw new ArgumentException("Only 8, 24 and 32 bpp images are supported.");
                }

                // Lock bitmap and return bitmap data
                bitmapData = source.LockBits(rect, ImageLockMode.ReadWrite,
                                             source.PixelFormat);

                // create byte array to copy pixel values
                int step = Depth / 8;
                Pixels = new byte[PixelCount * step];
                Iptr = bitmapData.Scan0;

                // Copy data from pointer to array
                Marshal.Copy(Iptr, Pixels, 0, Pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Unlock bitmap data
        /// </summary>
        public void UnlockBits()
        {
            try
            {
                // Copy data from byte array to pointer
                Marshal.Copy(Pixels, 0, Iptr, Pixels.Length);

                // Unlock bitmap data
                source.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Color GetPixel(int x, int y)
        {
            Color clr = Color.Empty;

            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (i > Pixels.Length - cCount)
                throw new IndexOutOfRangeException();

            if (Depth == 32) // For 32 bpp get Red, Green, Blue and Alpha
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                byte a = Pixels[i + 3]; // a
                clr = Color.FromArgb(a, r, g, b);
            }
            if (Depth == 24) // For 24 bpp get Red, Green and Blue
            {
                byte b = Pixels[i];
                byte g = Pixels[i + 1];
                byte r = Pixels[i + 2];
                clr = Color.FromArgb(r, g, b);
            }
            if (Depth == 8)
            // For 8 bpp get color value (Red, Green and Blue values are the same)
            {
                byte c = Pixels[i];
                clr = Color.FromArgb(c, c, c);
            }
            return clr;
        }

        /// <summary>
        /// Set the color of the specified pixel
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="color"></param>
        public void SetPixel(int x, int y, Color color)
        {
            // Get color components count
            int cCount = Depth / 8;

            // Get start index of the specified pixel
            int i = ((y * Width) + x) * cCount;

            if (Depth == 32) // For 32 bpp set Red, Green, Blue and Alpha
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
                Pixels[i + 3] = color.A;
            }
            if (Depth == 24) // For 24 bpp set Red, Green and Blue
            {
                Pixels[i] = color.B;
                Pixels[i + 1] = color.G;
                Pixels[i + 2] = color.R;
            }
            if (Depth == 8)
            // For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                Pixels[i] = color.B;
            }
        }
    }
    public class BitmapToAscii
    {
        public int RgbThreshold = 700; // rgb.R + rgb.G + rgb.B - maximum output is 765.
        public string StrFont = "0"; // The character representing the font 'black' colours.
        public string StrBackground = " "; // The character representing the background.
        public Bitmap BmpInput = new Bitmap(1, 1);
        public BitmapToAscii() { }
        public BitmapToAscii(Bitmap bmpInput, int rgbThreshold, string strFont, string strBackground)
        {
            this.BmpInput = bmpInput;
            this.RgbThreshold = rgbThreshold;
            this.StrFont = strFont;
            this.StrBackground = strBackground;
        }
        public string GetAscii()
        {
            StringBuilder inputConverted = new StringBuilder();
            LockBitmap lockBitmap = new LockBitmap(BmpInput);
            lockBitmap.LockBits();
            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    Color pixel = lockBitmap.GetPixel(x, y);
                    int pixelValue = pixel.R + pixel.G + pixel.B;

                    if (pixelValue <= RgbThreshold)
                    {
                        inputConverted.Append(StrFont);
                    }
                    else
                    {
                        inputConverted.Append(StrBackground);
                    }
                }
                inputConverted.Append(Environment.NewLine);
            }
            return inputConverted.ToString();
        }
        public void printAscii(ConsoleColor foregroundFont = ConsoleColor.White,
            ConsoleColor backgroundFont = ConsoleColor.Black,
            ConsoleColor foregroundBack = ConsoleColor.White,
            ConsoleColor backgroundBack = ConsoleColor.Black)
        {
            LockBitmap lockBitmap = new LockBitmap(BmpInput);
            lockBitmap.LockBits();
            for (int y = 0; y < lockBitmap.Height; y++)
            {
                for (int x = 0; x < lockBitmap.Width; x++)
                {
                    Color pixel = lockBitmap.GetPixel(x, y);
                    int pixelValue = pixel.R + pixel.G + pixel.B;

                    Console.SetCursorPosition(x, y);
                    Console.BackgroundColor = backgroundFont;
                    Console.ForegroundColor = foregroundFont;
                    if (pixelValue <= RgbThreshold)
                    {
                        Console.Write(StrFont);
                    }
                    else
                    {
                        Console.Write(StrBackground);
                    }
                }
            }
            lockBitmap.UnlockBits();
        }
    }
    public static class Extension
    {
        /// <summary>
        /// ASCIIFilter
        /// </summary>
        /// <param name="image"></param>
        /// <param name="ratio">1, 2, 10</param>
        /// <returns></returns>
        public static string ASCIIFilter(this Bitmap image, int ratio = 1)
        {
            bool toggle = false;
            StringBuilder sb = new StringBuilder();

            for (int h = 0; h < image.Height; h += ratio)
            {
                for (int w = 0; w < image.Width; w += ratio)
                {
                    Color pixelColor = image.GetPixel(w, h);
                    int red, green, blue;
                    red = green = blue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    Color grayColor = Color.FromArgb(red, green, blue);
                    if (!toggle)
                    {
                        int index = (grayColor.R * 10) / 255;
                        sb.Append(asciiChars[index]);
                    }
                }

                if (!toggle)
                {
                    sb.AppendLine();
                    toggle = true;
                }
                else
                {
                    toggle = false;
                }
            }

            return sb.ToString();
        }

        private static string[] asciiChars = { "#", "#", "@", "%", "=", "+", "*", ":", "-", ".", " " };

        public static string ASCIIFilter(this Bitmap sourceBitmap, int pixelBlockSize, int colorCount = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0, sourceBitmap.Width, sourceBitmap.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            StringBuilder asciiArt = new StringBuilder();

            int avgBlue = 0;
            int avgGreen = 0;
            int avgRed = 0;
            int offset = 0;

            int rows = sourceBitmap.Height / pixelBlockSize;
            int columns = sourceBitmap.Width / pixelBlockSize;

            if (colorCount > 0)
            {
                colorCharacters = GenerateRandomString(colorCount);
            }

            for (int y = 0; y < rows; y++)
            {
                for (int x = 0; x < columns; x++)
                {
                    avgBlue = 0;
                    avgGreen = 0;
                    avgRed = 0;

                    for (int pY = 0; pY < pixelBlockSize; pY++)
                    {
                        for (int pX = 0; pX < pixelBlockSize; pX++)
                        {
                            offset = y * pixelBlockSize * sourceData.Stride +
                                     x * pixelBlockSize * 4;

                            offset += pY * sourceData.Stride;
                            offset += pX * 4;

                            avgBlue += pixelBuffer[offset];
                            avgGreen += pixelBuffer[offset + 1];
                            avgRed += pixelBuffer[offset + 2];
                        }
                    }

                    avgBlue = avgBlue / (pixelBlockSize * pixelBlockSize);
                    avgGreen = avgGreen / (pixelBlockSize * pixelBlockSize);
                    avgRed = avgRed / (pixelBlockSize * pixelBlockSize);

                    asciiArt.Append(GetColorCharacter(avgBlue, avgGreen, avgRed));
                }

                asciiArt.Append("\r\n");
            }

            return asciiArt.ToString();
        }
        public static string RandomStringSort(this string stringValue)
        {
            char[] charArray = stringValue.ToCharArray();

            Random randomIndex = new Random((byte)charArray[0]);
            int iterator = charArray.Length;

            while (iterator > 1)
            {
                iterator -= 1;

                int nextIndex = randomIndex.Next(iterator + 1);

                char nextValue = charArray[nextIndex];
                charArray[nextIndex] = charArray[iterator];
                charArray[iterator] = nextValue;
            }

            return new string(charArray);
        }

        private static string colorCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        private static string GetColorCharacter(int blue, int green, int red)
        {
            string colorChar = string.Empty;
            int intensity = (blue + green + red) / 3 * (colorCharacters.Length - 1) / 255;

            colorChar = colorCharacters.Substring(intensity, 1).ToUpper();
            colorChar += colorChar.ToLower();

            return colorChar;
        }

        private static string GenerateRandomString(int maxSize)
        {
            StringBuilder stringBuilder = new StringBuilder(maxSize);
            Random randomChar = new Random();

            char charValue;

            for (int k = 0; k < maxSize; k++)
            {
                charValue = (char)(Math.Floor(255 * randomChar.NextDouble() * 4));

                if (stringBuilder.ToString().IndexOf(charValue) != -1)
                {
                    charValue = (char)(Math.Floor((byte)charValue * randomChar.NextDouble()));
                }

                if (Char.IsControl(charValue) == false &&
                    Char.IsPunctuation(charValue) == false &&
                    stringBuilder.ToString().IndexOf(charValue) == -1)
                {
                    stringBuilder.Append(charValue);

                    randomChar = new Random((int)((byte)charValue * (k + 1) + DateTime.Now.Ticks));
                }
                else
                {
                    randomChar = new Random((int)((byte)charValue * (k + 1) + DateTime.UtcNow.Ticks));
                    k -= 1;
                }
            }

            return stringBuilder.ToString().RandomStringSort();
        }
    }
}
