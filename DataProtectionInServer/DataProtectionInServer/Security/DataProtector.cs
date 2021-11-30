using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace DataProtectionInServer.Security
{
    public class DataProtector
    {
        private byte[] entropy;
        private string path;
        public DataProtector(string savedPath)
        {
            this.path = savedPath;

            entropy = new byte[16];

           new RNGCryptoServiceProvider().GetBytes(entropy);

            path += "EncryptedData.aero";
        }

        public int EncryptData(string criticalData)
        {
            var encodedData = UnicodeEncoding.ASCII.GetBytes(criticalData);
            FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate);
            //CryptoStream cryptoStream = new CryptoStream(fileStream, null, CryptoStreamMode.Write);
            int length = encryptDataToFile(encodedData, entropy, DataProtectionScope.CurrentUser, fileStream);
            fileStream.Close();
            return length;

        }

        private int encryptDataToFile(byte[] encodedData, byte[] entropy, DataProtectionScope currentUser, FileStream fileStream)
        {
            int output = 0;
            byte[] encryptedData = ProtectedData.Protect(encodedData, entropy, currentUser);
            if (fileStream.CanWrite && encryptedData!=null)
            {
                fileStream.Write(encryptedData, 0, encryptedData.Length);
                output = encryptedData.Length;
            }
            return output;
        }

        public string DecryptData(int length)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open);
            byte[] decryptData = decryptFromStream(entropy, DataProtectionScope.CurrentUser, fileStream, length);
            fileStream.Close();
            return UnicodeEncoding.ASCII.GetString(decryptData);
        }

        private byte[] decryptFromStream(byte[] entropy, DataProtectionScope currentUser, FileStream fileStream, int length)
        {
            byte[] inBuffer = new byte[length];
            byte[] outBuffer;

            if (fileStream.CanWrite)
            {
                fileStream.Read(inBuffer, 0, inBuffer.Length);
                outBuffer = ProtectedData.Unprotect(inBuffer, entropy, currentUser);

            }
            else
            {
                throw new IOException("Streaming üzerine yazılamıyor!");
            }

            return outBuffer;
        }
    }
}
