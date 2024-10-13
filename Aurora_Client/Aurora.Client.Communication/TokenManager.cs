using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    internal class TokenManager
    {
        private static TokenManager _instance;
        public static TokenManager Instance
        {
            get
            {
                _instance ??= new TokenManager();
                return _instance;
            }
        }
        private static readonly string tokenFilePath = "token.dat";

        public async Task SaveTokenToFileAsync(string decryptedToken)
        {
            var encryptedToken = TokenEncryptionManager.Instance.EncryptTokenToFileAsync(decryptedToken);
            if (!File.Exists(tokenFilePath))
            {
                File.Create(tokenFilePath);
            }
            await File.WriteAllTextAsync(tokenFilePath, encryptedToken);
        }

        public async Task<string> LoadTokenFromFile()
        {
            var encryptedToken = await File.ReadAllTextAsync(tokenFilePath);
            return TokenEncryptionManager.Instance.DecryptTokenToFileAsync(encryptedToken);
        }
    }
}
