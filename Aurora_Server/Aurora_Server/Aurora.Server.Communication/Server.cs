using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    sealed class Server
    {
        private Server _instance;
        public Server Instance
        {
            get
            {
                _instance ??= new Server();
                return _instance;
            }
        }
        public void RunServer()
        {
            var code = Console.ReadLine();
            while(code is not "EXIT")
            {
                code = Console.ReadLine();
            }
        }
    }
}
