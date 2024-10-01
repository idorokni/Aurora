using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    public enum ResponseCode : byte
    {
        TOKEN_SIGNUP_SUCCESS = 100,
        TOKEN_SIGNUP_FAILED = 101,
        TOKEN_LOGIN_SUCCESS = 200,
        TOKEN_LOGIN_FAILED = 201
    }
}
