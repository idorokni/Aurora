using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using Aurora.Server.Database.Data;
using System.Net.Http.Headers;

namespace Aurora.Server.Communication
{
    public class JWTLoginRequestHandler : IRequestHandler
    {
        public bool CheckIfMatchRegex(string username, string password, string email = null)
        {
            bool isValid = true;

            isValid = Constants.PasswordRegex.IsMatch(password);
            if(email == null)
            {
                return isValid;
            } 

            isValid = Constants.EmailRegex.IsMatch(email) && isValid;
            return isValid;
        }

        public bool IsRequestValid(RequestInfo info)
        {
            return info.code == RequestCode.LOGIN_REQUEST_CODE || info.code == RequestCode.SIGN_UP_REQUEST_CODE || info.code == RequestCode.CONNECT_REQUEST_CODE;
        }
        public async Task<(IRequestHandler, ResponseInfo)> HandleRequest(RequestInfo info)
        {
            ResponseInfo result;

            switch (info.code)
            {
                case RequestCode.LOGIN_REQUEST_CODE:
                    var loginData = JsonSerializer.Deserialize<LoginData>(info.data);
                    if (loginData == null)
                    {
                        result.message = "error loging in";
                        result.code = ResponseCode.TOKEN_LOGIN_FAILED;
                        break;
                    }

                    if(!CheckIfMatchRegex(loginData.Username, loginData.Password))
                    {
                        result.message = "Password not strong enough";
                        result.code = ResponseCode.TOKEN_LOGIN_FAILED;
                        break;
                    }

                    if (!DatabaseManager.Instance.UserExists(loginData.Username) || !DatabaseManager.Instance.checkIfPasswordsMatch(loginData.Username, loginData.Password))
                    {
                        result.message = "user and password dont match";
                        result.code = ResponseCode.TOKEN_LOGIN_FAILED;
                        break;
                    }

                    result.message = JsonSerializer.Serialize(await JWTLoginManager.Instance.JWTLoginAsync(loginData.Username, loginData.Password));
                    result.code = ResponseCode.TOKEN_LOGIN_SUCCESS;
                    break;

                case RequestCode.SIGN_UP_REQUEST_CODE:
                    var signupData = JsonSerializer.Deserialize<SignupData>(info.data);
                    if (signupData == null)
                    {
                        result.message = "Invalid signup data";
                        result.code = ResponseCode.TOKEN_SIGNUP_FAILED;
                        break;
                    }

                    if (!CheckIfMatchRegex(signupData.Username,signupData.Password, signupData.Email))
                    {
                        result.message = "Password not strong enough or email is not in correct format";
                        result.code = ResponseCode.TOKEN_SIGNUP_FAILED;
                        break;
                    }

                    if (DatabaseManager.Instance.UserExists(signupData.Username))
                    {
                        result.message = "user already exists";
                        result.code = ResponseCode.TOKEN_SIGNUP_FAILED;
                        break;
                    }

                    result.message = await JWTLoginManager.Instance.JWTSignupAsync(signupData.Username, signupData.Password, signupData.Email);
                    result.code = ResponseCode.TOKEN_SIGNUP_SUCCESS;
                    DatabaseManager.Instance.AddUser(signupData.Username, signupData.Password, signupData.Email);
                    break;

                case RequestCode.CONNECT_REQUEST_CODE:
                    string jwtToken = info.data;
                    var user = await JWTLoginManager.Instance.JWTConnectAsync(jwtToken);

                    if(user == null)
                    {
                        result.message = "cant connect";
                        result.code = ResponseCode.TOKEN_CONNECT_FAILED;
                        break;
                    }

                    result.message = JsonSerializer.Serialize(user);
                    result.code = ResponseCode.TOKEN_CONNECT_SUCCESS;
                    break;

                default:
                    result.message = "";
                    result.code = ResponseCode.TOKEN_LOGIN_FAILED;
                    break;
            }

            return (RequestHandlerFactory.Instance.GetJWTRequestHandler(), result);
        }
    }
}