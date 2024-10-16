using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    public class TokenEncryptionManager
    {
        private static TokenEncryptionManager _instance;
        public static TokenEncryptionManager Instance
        {
            get
            {
                _instance ??= new TokenEncryptionManager();
                return _instance;
            }
        }

        public string EncryptTokenToFileAsync(string decryptedToken)
        {
            return decryptedToken;
        }

        public string DecryptTokenToFileAsync(string encryptedToken)
        {
            return encryptedToken;
        }
    }
}
