using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Client.Communication
{
    public class Communicator
    {
        private static Communicator? _instance;
        private TcpClient _client;
        private NetworkStream? _stream;
        private int CONNECTION_PORT = 1223;
        private string CONNECTION_IP = "127.0.0.1";
        private int BUFFER_SIZE = 1024;

        public static Communicator Instance
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
            await _client.GetStream().WriteAsync(dataToSend);
        }

        private async Task<string> ReadMessageFromServer()
        {
            var buffer = new byte[BUFFER_SIZE];
            var bytesRead = await _client.GetStream().ReadAsync(buffer, 0, buffer.Length);
            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }



        public async void ConnectToServerAsync()
        {
            try
            {
                await _client.ConnectAsync(CONNECTION_IP, CONNECTION_PORT);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Communication could not be established due to {ex.Message}");
            }
        }


    }
}
