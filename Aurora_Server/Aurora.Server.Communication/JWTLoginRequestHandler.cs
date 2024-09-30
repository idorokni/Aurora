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
            return info.code is RequestCode.LOGIN_REQUEST_CODE && info.code is RequestCode.SIGN_UP_REQUEST_CODE;
        }
        public async Task<IRequestHandler> HandleRequest(RequestInfo info)
        {

        }
    }
}
