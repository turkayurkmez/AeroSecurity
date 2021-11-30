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
    }
}
