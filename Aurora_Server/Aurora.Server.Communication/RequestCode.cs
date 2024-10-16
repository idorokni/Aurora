using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public enum RequestCode : byte
    {
        LOGIN_REQUEST_CODE = 51,
        SIGN_UP_REQUEST_CODE = 52,
        CONNECT_REQUEST_CODE = 53
    }
}
