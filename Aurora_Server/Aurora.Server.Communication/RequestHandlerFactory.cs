using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public class RequestHandlerFactory
    {
        private static RequestHandlerFactory _instance;

        public static RequestHandlerFactory Instance
        {
            get
            {
                _instance ??= new RequestHandlerFactory();
                return _instance;
            }
        }

        public JWTLoginRequestHandler GetJWTLoginManager() => new JWTLoginRequestHandler();
    }
}
