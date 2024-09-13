using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    sealed class Communicator
    {
        private Communicator _instance;
        private List<TcpClient> _clients;
        private TcpListener _server;
        private int SERVER_LISTEN_PORT = 1223;
        private int BUFFER_SIZE = 1024;

        public Communicator()
        {
            _clients = new List<TcpClient>();
            _server = new TcpListener(IPAddress.Any, SERVER_LISTEN_PORT);
            _server.Start();
        }

        public Communicator Instance
        {
            get
            {
                _instance ??= new Communicator();
                return _instance;  
            }
        }

        public async Task AcceptClients()
        {
            while (true)
            {
                _clients.Add(await _server.AcceptTcpClientAsync());
                _ = Task.Run(() => { _ = HandleClientAsync(_clients.Last()); });
            }
        }

        private async Task HandleClientAsync(TcpClient client)
        {
            var stream = client.GetStream();
            var buffer = new byte[BUFFER_SIZE];
            var bytesRead = await stream.ReadAsync(buffer, 0, BUFFER_SIZE);
            while (client.Connected && bytesRead is not 0)
            {
                var receivedMessage = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                var response = Encoding.UTF8.GetBytes(receivedMessage);
                await stream.WriteAsync(response, 0, response.Length);
            }
        }
    }
}
