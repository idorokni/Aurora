using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    public class AuthenticationManagerAurora
    {
        private static AuthenticationManagerAurora _instance = null!;
        public static AuthenticationManagerAurora Instance
        {
            get
            {
                _instance ??= new AuthenticationManagerAurora();
                return _instance;
            }
        }

        //public async Task<string> SigninToServer(string username, string password)
        //{
        //    RequestInfo requestInfo;
        //    requestInfo.message = username + "@" + password;
        //    requestInfo.code = RequestCode.LOGIN_REQUEST_CODE;
        //    await Communicator.Instance.SendMessageToServer(requestInfo);
        //    ResponseInfo responseInfo = await Communicator.Instance.ReadMessageFromServer();
        //    return responseInfo;
        //}
        
        public async Task<ResponseInfo> SignupToServer(string username, string password, string email)
        {
            RequestInfo requestInfo;
            requestInfo.message = username + "@" + password + "@" + email;
            requestInfo.code = RequestCode.SIGN_UP_REQUEST_CODE;
            await Communicator.Instance.SendMessageToServer(requestInfo);
            ResponseInfo responseInfo = await Communicator.Instance.ReadMessageFromServer();
            if(responseInfo.code == ResponseCode.TOKEN_SIGNUP_SUCCESS)
            {
                await TokenManager.Instance.SaveTokenToFileAsync(responseInfo.message);
            }
            return responseInfo;
        }
    }
}
