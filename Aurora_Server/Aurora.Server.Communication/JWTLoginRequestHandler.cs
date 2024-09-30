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
        public async Task<IRequestHandler> HandleRequest(RequestInfo info)
        {

            return await Task.Run(async () =>
            {
                switch (info.code)
                {
                    case RequestCode.LOGIN_REQUEST_CODE:
                        await JWTLoginManager.Instance.JWTloginAsync(info.data);
                        break;
                    case RequestCode.SIGN_UP_REQUEST_CODE:
                        string username = info.data.Split("###")[0];
                        string password = info.data.Split("###")[1];
                        await JWTLoginManager.Instance.JWTsignupAsync(username, password);
                        break;
                    default:
                        break;
                } 
                return RequestHandlerFactory.Instance.GetJWTRequestHandler();// TODO: switch to new handler
            });
        }
    }
}