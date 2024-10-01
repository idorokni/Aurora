﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public struct RequestResult
    {
        public ResponseCode code;
        public string? message;
    }
}
