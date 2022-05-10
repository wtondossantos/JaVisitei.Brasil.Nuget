using System;

namespace JaVisitei.Brasil.Helper.Others
{
    public static class Utility
    {
        public static string RandomHexString(string index)
        {
            var random = new Random().Next(0, int.MaxValue).ToString(index);

            return random;
        }

        public static string RandomColorRBGString()
        {
            var random = new Random();
            string color = String.Empty;

            for (int i = 0; i < 2; i++)
                color += $"{random.Next(222)},";

            color += $"{random.Next(222)}";

            return color;
        }

        public static string RandomAlphanumericString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            Random random = new Random();

            string result = String.Empty;
            while (0 < length--)
                result += valid[random.Next(valid.Length)];

            return result;
        }
    }
}
