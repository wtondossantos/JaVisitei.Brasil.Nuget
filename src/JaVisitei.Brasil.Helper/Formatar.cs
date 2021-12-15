using System;
using System.Collections.Generic;
using System.Text;

namespace JaVisitei.Brasil.Helper
{
    public static class Formatar
    {
        public static string ByteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }
    }
}
