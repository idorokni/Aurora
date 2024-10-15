using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public class HomeRequestHandler : IRequestHandler
    {
        public bool IsRequestValid(RequestInfo info)
        {

        }
        public Task<(IRequestHandler, ResponseInfo)> HandleRequest(RequestInfo info);
    }
}
