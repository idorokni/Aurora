using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;

namespace Aurora.Server.Communication
{
    public class JWTLoginRequestHandler : IRequestHandler
    {
        public bool IsRequestValid(RequestInfo info)
        {
            return info.code == RequestCode.LOGIN_REQUEST_CODE || info.code == RequestCode.SIGN_UP_REQUEST_CODE;
        }
        public async Task<(IRequestHandler, ResponseInfo)> HandleRequest(RequestInfo info)
        {
            ResponseInfo result = new ResponseInfo();

            switch (info.code)
            {
                case RequestCode.LOGIN_REQUEST_CODE:
                    await JWTLoginManager.Instance.JWTloginAsync(info.data);
                    break;
                case RequestCode.SIGN_UP_REQUEST_CODE:
                    string username = info.data.Split("@")[0];
                    string password = info.data.Split("@")[1];
                    string email = info.data.Split("@")[2];
                    result.message = await JWTLoginManager.Instance.JWTsignupAsync(username, password, email);
                    result.code = result.message != "" ? ResponseCode.TOKEN_SIGNUP_SUCCESS : ResponseCode.TOKEN_SIGNUP_FAILED;
                    break;
                default:
                    break;
            } 
            return (RequestHandlerFactory.Instance.GetJWTRequestHandler(), result);// TODO: switch to new handler
        }
    }
}