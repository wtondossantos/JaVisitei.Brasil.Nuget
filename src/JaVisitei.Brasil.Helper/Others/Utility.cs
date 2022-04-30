using System;

namespace JaVisitei.Brasil.Helper.Others
{
    public static class Utility
    {
        public static string RandomHexString()
        {
            Random rdm = new Random();
            string hexValue = string.Empty;
            int num;

            for (int i = 0; i < 8; i++)
            {
                num = rdm.Next(0, int.MaxValue);
                hexValue += num.ToString("X6");
            }

            return hexValue;
        }
    }
}
