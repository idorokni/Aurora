using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aurora.Server.Database;

namespace Aurora.Server.Communication
{
    sealed class Server
    {
        private static Server _instance;
        public static Server Instance
        {
            get
            {
                _instance ??= new Server();
                return _instance;
            }
        }
        public void RunServer()
        {
            Task.Run(() => { _ = Communicator.Instance.AcceptClients(); });
            var code = Console.ReadLine();
            while(code is not "EXIT")
            {
                code = Console.ReadLine();
            }
        }
    }
}
