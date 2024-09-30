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

        public async Task<string> EncryptTokenToFileAsync(string decryptedToken)
        {
            await Task.Run(() =>
            {
                var tokenBytes = Encoding.UTF8.GetBytes(decryptedToken);
                return Convert.ToBase64String(tokenBytes);
            });
            return decryptedToken;
        }

        public async Task<string> DecryptTokenToFileAsync(string encryptedToken)
        {
            await Task.Run(() =>
            {
                var encryptedBytes = Convert.FromBase64String(encryptedToken);
                return Encoding.UTF8.GetString(encryptedBytes);
            });
            return encryptedToken;
        }
    }
}
