using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    sealed class Communicator
    {
        private Communicator _instance;
        private TcpClient _client;
        private NetworkStream _stream;
        private int CONNECTION_PORT = 1223;
        private string CONNECTION_IP = "127.0.0.1";
        private int BUFFER_SIZE = 1024;
            
        public Communicator Instance
        {
            get
            {
                _instance ??= new Communicator();
                return _instance;
            }
        }

        public Communicator()
        {
            _client = new TcpClient();
        }

        private async Task SendMessageToServer(string message)
        {
            var dataToSend = Encoding.UTF8.GetBytes(message);
            await _stream.WriteAsync(dataToSend, 0, dataToSend.Length);
        }

        private async Task<string> ReadMessageFromServer()
        {
            var buffer = new byte[BUFFER_SIZE];
            var bytesRead = await _stream.ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }


            
        public async Task ConnectToServer()
        {
            try
            {
                await _client.ConnectAsync(CONNECTION_IP, CONNECTION_PORT);
                _stream = _client.GetStream();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Communication could not be established due to {ex.Message}");
                _stream = null;
            }
        }

            
    }
}
