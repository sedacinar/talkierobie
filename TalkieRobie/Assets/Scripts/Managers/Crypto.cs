using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
namespace Sienar.TalkieRobie.Managers
{
    public static class Crypto
    {
        private static readonly string encryptionKey = "SienarGameToywar"; // 16, 24 veya 32 karakter uzunluğunda olmalıdır

        public static string Encrypt(string plainText)
        {
            byte[] key = Encoding.UTF8.GetBytes(encryptionKey);
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.GenerateIV();
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
                    msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                    }
                    return Convert.ToBase64String(msEncrypt.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText)
        {
            byte[] fullCipher = Convert.FromBase64String(cipherText);
            byte[] key = Encoding.UTF8.GetBytes(encryptionKey);

            using (MemoryStream msDecrypt = new MemoryStream(fullCipher))
            {
                byte[] iv = new byte[sizeof(int)];
                msDecrypt.Read(iv, 0, iv.Length);
                int ivLength = BitConverter.ToInt32(iv, 0);

                iv = new byte[ivLength];
                msDecrypt.Read(iv, 0, iv.Length);

                using (Aes aesAlg = Aes.Create())
                {
                    aesAlg.Key = key;
                    aesAlg.IV = iv;
                    ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            return srDecrypt.ReadToEnd();
                        }
                    }
                }
            }
        }
    }
}