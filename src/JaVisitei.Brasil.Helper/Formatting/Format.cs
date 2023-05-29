using System;
using System.Drawing;
using System.Text;

namespace JaVisitei.Brasil.Helper.Formatting
{
    public static class Format
    {
        public static string ByteArrayToString(byte[] inputArray)
        {
            if (inputArray is null)
                throw new ArgumentNullException(nameof(inputArray));

            var output = new StringBuilder("");

            for (int i = 0; i < inputArray.Length; i++)
                output.Append(inputArray[i].ToString("X2"));

            return output.ToString();
        }

        public static string EmailTraceabilityCodeString(int primary, int secundary)
        {
            return $"{primary:0000000000}{secundary:00000}";
        }

        public static string ConvertRGBToHex(string rgb)
        {
            if(string.IsNullOrEmpty(rgb))
                return rgb;

            var splitRGB = rgb.Split(',');
            var color = Color.FromArgb(Convert.ToInt16(splitRGB[0]), Convert.ToInt16(splitRGB[1]), Convert.ToInt16(splitRGB[2]));
            return $"#{color.R:X2}{color.G:X2}{color.B:X2}";
        }
    }
}
