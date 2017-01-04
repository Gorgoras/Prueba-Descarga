using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using System.Security.Cryptography;


namespace Prueba_Descarga.Helpers
{
    static class EncryptionHelper
    {
        private static string pass = "GBmEYMV9"; //hardcoded random encryption pass

        public static void saveEncrypted(byte[] textToencrypt, string usuario)
        {
            FileStream fileToWrite = new FileStream(@"e:\Pruebas\" + $"{usuario}.enc", FileMode.Create, FileAccess.Write);

            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            desc.Key = Encoding.ASCII.GetBytes(pass);
            desc.IV = Encoding.ASCII.GetBytes(pass);

            ICryptoTransform ict = desc.CreateEncryptor();
            CryptoStream cryptStr = new CryptoStream(fileToWrite, ict, CryptoStreamMode.Write);
            try
            {
                cryptStr.Write(textToencrypt, 0, textToencrypt.Length);
            }
            catch { }

        }

        public static byte[] loadEncrypted(string usuario)
        {
            byte[] resp = null;

            FileStream fileToRead = new FileStream(@"e:\Pruebas\" + $"{usuario}.enc", FileMode.Open, FileAccess.Read);

            DESCryptoServiceProvider desc = new DESCryptoServiceProvider();
            desc.Key = Encoding.ASCII.GetBytes(pass);
            desc.IV = Encoding.ASCII.GetBytes(pass);

            ICryptoTransform ict = desc.CreateDecryptor();
            CryptoStream cryptStr = new CryptoStream(fileToRead, ict, CryptoStreamMode.Read);

            StreamReader strRdr = new StreamReader(cryptStr);
            try
            {
                string content = strRdr.ReadToEnd();
                resp = Encoding.ASCII.GetBytes(content);
            }
            catch { }

            return resp;
        }
    }
}
