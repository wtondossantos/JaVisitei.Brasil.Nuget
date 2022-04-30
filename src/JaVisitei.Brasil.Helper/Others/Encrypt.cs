using System.Text;
using System.Security.Cryptography;
using JaVisitei.Brasil.Helper.Formatting;

namespace JaVisitei.Brasil.Helper.Others
{
    public static class Encrypt
    {
        public static string Sha256encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256 sha256hasher = SHA256.Create();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(frase));
            return Format.ByteArrayToString(hashedDataBytes);
        }
    }
}
