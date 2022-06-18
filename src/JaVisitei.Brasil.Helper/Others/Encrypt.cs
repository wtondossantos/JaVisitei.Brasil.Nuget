using System.Text;
using System.Security.Cryptography;
using JaVisitei.Brasil.Helper.Formatting;
using System;

namespace JaVisitei.Brasil.Helper.Others
{
    public static class Encrypt
    {
        public static string Sha256encrypt(string phrase)
        {
            if (phrase is null)
                throw new ArgumentNullException(nameof(phrase));

            var sha256hasher = SHA256.Create();
            var hashedDataBytes = sha256hasher.ComputeHash(new UTF8Encoding().GetBytes(phrase));
            return Format.ByteArrayToString(hashedDataBytes);
        }
    }
}
