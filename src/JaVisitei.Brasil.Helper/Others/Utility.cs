using System;
using System.Text;

namespace JaVisitei.Brasil.Helper.Others
{
    public static class Utility
    {
        public static string RandomHexString(string index)
        {
            if (index is null)
                throw new ArgumentNullException(nameof(index));

            return new Random().Next(0, int.MaxValue).ToString(index);
        }

        public static string RandomColorRBGString()
        {
            var random = new Random();
            var color = new StringBuilder("");

            for (int i = 0; i < 2; i++)
                color.Append($"{random.Next(222)},");

            color.Append($"{random.Next(222)}");

            return color.ToString();
        }

        public static string RandomAlphanumericString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var random = new Random();
            var output = new StringBuilder("");

            while (0 < length--)
                output.Append(valid[random.Next(valid.Length)]);

            return output.ToString();
        }
    }
}
