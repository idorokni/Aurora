using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;

namespace Aurora.Server.Communication
{
    public class JWTLoginRequestHandler : IRequestHandler
    {
        public bool IsRequestValid(RequestInfo info)
        {
            return info.code == RequestCode.LOGIN_REQUEST_CODE || info.code == RequestCode.SIGN_UP_REQUEST_CODE || info.code == RequestCode.CONNECT_REQUEST_CODE;
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
                    try
                    {
                        var signupData = JsonSerializer.Deserialize<SignupData>(info.data);
                        if (signupData != null)
                        {
                            result.message = await JWTLoginManager.Instance.JWTSignupAndSigninAsync(
                                signupData.Username, signupData.Password, signupData.Email);

                            result.code = !string.IsNullOrEmpty(result.message)
                                ? ResponseCode.TOKEN_SIGNUP_SUCCESS
                                : ResponseCode.TOKEN_SIGNUP_FAILED;
                        }
                        else
                        {
                            result.message = "Invalid signup data";
                            result.code = ResponseCode.TOKEN_SIGNUP_FAILED;
                        }
                    }
                    catch (JsonException)
                    {
                        result.message = "Error parsing signup data";
                        result.code = ResponseCode.TOKEN_SIGNUP_FAILED;
                    }
                    break;

                case RequestCode.CONNECT_REQUEST_CODE:
                    string jwtToken = info.data;
                    var user = await JWTLoginManager.Instance.JWTloginAsync(jwtToken);
                    if (user != null)
                    {
                        result.message = JsonSerializer.Serialize(user);
                        result.code = ResponseCode.TOKEN_CONNECT_SUCCESS;
                    }
                    else
                    {
                        result.message = "cant connect";
                        result.code = ResponseCode.TOKEN_CONNECT_FAILED;
                    }
                    break;

                default:
                    break;
            }

            return (RequestHandlerFactory.Instance.GetJWTRequestHandler(), result);
        }
    }
}