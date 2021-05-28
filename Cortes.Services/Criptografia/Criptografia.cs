using System;
using System.Security.Cryptography;
using System.Text;

namespace Cortes.Services
{
    public static class Criptografia
    {
        private static RijndaelManaged rijndael = new RijndaelManaged();
        private static byte[] passBytes;
        private static byte[] encryptionkeyBytes;


        /// <summary>
        /// Método que criptografa o texto passado como parâmetro
        /// </summary>
        /// <param name="textToEncrypt"></param>
        /// <returns>Uma string criptografada</returns>
        public static string Encrypt(string textToEncrypt)
        {
            string encryptionKey = "felipemattiolidossantos";
            passBytes = Encoding.UTF8.GetBytes(encryptionKey);
            SetOperation();
            SetBeginCryptography();

            byte[] textDataByte = Encoding.UTF8.GetBytes(textToEncrypt);

            ICryptoTransform objtransform = rijndael.CreateEncryptor();
            return Convert.ToBase64String(objtransform.TransformFinalBlock(textDataByte, 0, textDataByte.Length));
        }

        /// <summary>
        /// Método que descriptografa o texto criptografado
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns>Uma string descriptografada</returns>
        public static string Decrypt(string encryptedText)
        {
            SetOperation();

            byte[] encryptedTextByte = Convert.FromBase64String(encryptedText);
            encryptionkeyBytes = new byte[0x10];

            SetBeginCryptography();

            byte[] textByte = rijndael.CreateDecryptor().TransformFinalBlock(encryptedTextByte, 0, encryptedTextByte.Length);
            return Encoding.UTF8.GetString(textByte);
        }

        private static void SetOperation()
        {
            rijndael.Mode = CipherMode.CBC;
            rijndael.Padding = PaddingMode.PKCS7;
            rijndael.KeySize = 0x80;
            rijndael.BlockSize = 0x80;
        }

        private static void SetBeginCryptography()
        {
            encryptionkeyBytes = new byte[] { 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00 };

            int len = passBytes.Length;
            if (len > encryptionkeyBytes.Length)
            {
                len = encryptionkeyBytes.Length;
            }

            Array.Copy(passBytes, encryptionkeyBytes, len);

            rijndael.Key = encryptionkeyBytes;
            rijndael.IV = encryptionkeyBytes;
        }
    }
}

