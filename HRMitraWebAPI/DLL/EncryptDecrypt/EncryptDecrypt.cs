using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace NEncryptDecrypt
{
    public class EncryptDecrypt
    {
        /// <summary>
        /// Encrypts a given password and returns the encrypted string
        /// </summary>
        /// <param name="clearText"></param>
        /// <returns></returns>
        public static string Encrypt(string clearText)
        {
            string EncryptionKey = "BeSpoke";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

        /// <summary>
        /// Decrypts a given string.
        /// </summary>
        /// <param name="cipherText"></param>
        /// <returns></returns>
        public static string Decrypt(string cipherText)
        {
            string EncryptionKey = "BeSpoke";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        /// <summary>
        /// Read connection string from config file
        /// </summary>
        /// <returns></returns>
        public static string GetConnString()
        {
            string conStr = "";
            try
            {
                //string path = AppDomain.CurrentDomain.BaseDirectory;
                //XmlDocument xmldoc = new XmlDocument();

                //FileStream fs = new FileStream(path + "\\DataBaseConfig.xml", FileMode.Open, FileAccess.Read);
                //xmldoc.Load(fs);
                //conStr = xmldoc.InnerText;
                conStr = ConfigurationManager.ConnectionStrings["DbConnection"].ConnectionString;

                //int index1 = conStr.IndexOf("Password=") + 9;
                //int index2 = conStr.IndexOf(";", index1);
                //string pwd = conStr.Substring(index1, (index2 - index1));

                //string dencryptPwd = Decrypt(pwd);
                //conStr = conStr.Replace(pwd, dencryptPwd);
            }
            catch
            {
            }
            return conStr;
        }
    }
}
