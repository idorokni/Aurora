using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    internal class TokenManager
    {
        private TokenManager _instance;
        public TokenManager Instance
        {
            get
            {
                _instance ??= new TokenManager();
                return _instance;
            }
        }
        private static readonly string TokenFilePath = "token.dat";

        public async Task SaveTokenToFileAsync(string decryptedToken)
        {
            var encryptedToken = await TokenEncryptionManager.Instance.EncryptTokenToFileAsync(decryptedToken);
            await File.WriteAllTextAsync(TokenFilePath, encryptedToken);
        }

        public async Task<string> LoadTokenFromFile()
        {
            var encryptedToken = await File.ReadAllTextAsync(TokenFilePath);
            return await TokenEncryptionManager.Instance.DecryptTokenToFileAsync(encryptedToken);
        }
    }
}
