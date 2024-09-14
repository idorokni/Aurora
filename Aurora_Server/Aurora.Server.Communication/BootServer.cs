using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    internal class BootServer
    {
        static void Main(string[] args) => Server.Instance.RunServer();
    }
}
