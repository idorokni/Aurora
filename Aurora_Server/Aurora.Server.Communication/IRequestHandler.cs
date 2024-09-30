using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    interface IRequestHandler
    {
        bool IsRequestValid(RequestInfo info);
        Task<IRequestHandler> HandleRequest(RequestInfo info);
    }
}
