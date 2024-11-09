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
        public async Task<string?> JWTSignupAndSigninAsync(string username, string password, string email)
        {
            try
            {
                return await JWTService.GenerateTokenAsync(username, password, email);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<LoggedUser> JWTloginAsync(string token)
        {
            return await JWTService.ValidateTokenAsync(token);
        }
    }
}
