using Microsoft.Identity.Client;
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
        public async Task<string?> JWTSignupAsync(string username, string password, string email)
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

        public async Task<LoginReturnData> JWTLoginAsync(string username, string password)
        {
            try
            {
                var loginReturnData = new LoginReturnData();
                loginReturnData.Email = DatabaseManager.Instance.FindEmail(username);
                loginReturnData.Token = await JWTService.GenerateTokenAsync(username, password, loginReturnData.Email);

                return loginReturnData;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<LoggedUser> JWTConnectAsync(string token)
        {
            return await JWTService.ValidateTokenAsync(token);
        }
    }
}
