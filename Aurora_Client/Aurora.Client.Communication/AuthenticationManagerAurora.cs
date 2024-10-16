using System;
using System.Text.Json;
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

        public async Task<ResponseInfo> TrySigninginToServerWithToken()
        {
            ResponseInfo responseInfo = new ResponseInfo();
            RequestInfo requestInfo = new RequestInfo();

            if (!TokenManager.Instance.IsTokenExist())
            {
                responseInfo.code = ResponseCode.TOKEN_CONNECT_FAILED;
                responseInfo.message = "Cannot connect";
                return responseInfo;
            }

            requestInfo.message = await TokenManager.Instance.LoadTokenFromFile();
            requestInfo.code = RequestCode.CONNECT_REQUEST_CODE;
            await Communicator.Instance.SendMessageToServer(requestInfo);
            responseInfo = await Communicator.Instance.ReadMessageFromServer();
            return responseInfo;
        }

        public async Task<ResponseInfo> SigninToServer(string username, string password)
        {
            RequestInfo requestInfo = new RequestInfo
            {
                message = JsonSerializer.Serialize(new {Username = username, Password = password}),
                code = RequestCode.LOGIN_REQUEST_CODE
            };

            await Communicator.Instance.SendMessageToServer(requestInfo);
            var responseInfo = await Communicator.Instance.ReadMessageFromServer();

            if (responseInfo.code == ResponseCode.TOKEN_LOGIN_SUCCESS)
            {
                await TokenManager.Instance.SaveTokenToFileAsync(responseInfo.message);
            }

            return responseInfo;
        }

        public async Task<ResponseInfo> SignupToServer(string username, string password, string email)
        {
            RequestInfo requestInfo = new RequestInfo
            {
                message = JsonSerializer.Serialize(new { Username = username, Password = password, Email = email }),
                code = RequestCode.SIGN_UP_REQUEST_CODE
            };

            await Communicator.Instance.SendMessageToServer(requestInfo);
            var responseInfo = await Communicator.Instance.ReadMessageFromServer();

            if (responseInfo.code == ResponseCode.TOKEN_SIGNUP_SUCCESS)
            {
                await TokenManager.Instance.SaveTokenToFileAsync(responseInfo.message);
            }

            return responseInfo;
        }
    }
}
