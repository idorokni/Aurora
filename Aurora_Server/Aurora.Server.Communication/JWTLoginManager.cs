using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public class JWTLoginManager
    {
        private static JWTLoginManager _instance;
        public static JWTLoginManager Instance { 
            get
            {
                _instance ??= new JWTLoginManager();
                return _instance;
            }
        }
        public async Task<string?> JWTsignupAsync(string username, string password)
        {
            try
            {
                return await JWTService.GenerateTokenAsync(username, password);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<bool> JWTloginAsync(string token)
        {
            return await JWTService.ValidateTokenAsync(token);
        }
    }
}
