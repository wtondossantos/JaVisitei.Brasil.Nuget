using System.Text;
using System.Security.Cryptography;
using System;

namespace JaVisitei.Brasil.Helper
{
    public static class Encriptar
    {
        public static string Md5encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            MD5CryptoServiceProvider md5hasher = new MD5CryptoServiceProvider();
            byte[] hashedDataBytes = md5hasher.ComputeHash(encoder.GetBytes(frase));
            return Formatar.ByteArrayToString(hashedDataBytes);
        }

        public static string Sha1encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA1CryptoServiceProvider sha1hasher = new SHA1CryptoServiceProvider();
            byte[] hashedDataBytes = sha1hasher.ComputeHash(encoder.GetBytes(frase));
            return Formatar.ByteArrayToString(hashedDataBytes);
        }

        public static string Sha256encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(frase));
            return Formatar.ByteArrayToString(hashedDataBytes);
        }

        public static string Sha384encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA384Managed sha384hasher = new SHA384Managed();
            byte[] hashedDataBytes = sha384hasher.ComputeHash(encoder.GetBytes(frase));
            return Formatar.ByteArrayToString(hashedDataBytes);
        }

        public static string Sha512encrypt(string frase)
        {
            UTF8Encoding encoder = new UTF8Encoding();
            SHA512Managed sha512hasher = new SHA512Managed();
            byte[] hashedDataBytes = sha512hasher.ComputeHash(encoder.GetBytes(frase));
            return Formatar.ByteArrayToString(hashedDataBytes);
        }
    }
}
