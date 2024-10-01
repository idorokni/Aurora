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
        private readonly int HEADER_SIZE = 5;

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

        private async Task SendMessageToServer(RequestInfo info)
        {
            var wholeMessage = new byte[HEADER_SIZE + info.message.Length];
            wholeMessage[0] = (byte)info.code;
            Array.Copy(BitConverter.GetBytes(info.message.Length), 0, wholeMessage, 1, 4);
            Array.Copy(Encoding.UTF8.GetBytes(info.message), 0, wholeMessage, HEADER_SIZE, info.message.Length);
            await _client.GetStream().WriteAsync(wholeMessage);
        }

        private async Task<string> ReadMessageFromServer()
        {
            var stream = _client.GetStream();

            return Encoding.UTF8.GetString(buffer, 0, bytesRead);
        }



        public async void ConnectToServerAsync()
        {
            try
            {
                await _client.ConnectAsync(CONNECTION_IP, CONNECTION_PORT);
                await SendMessageToServer(RequestCode.SIGN_UP_REQUEST_CODE, "idodi###12345");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Communication could not be established due to {ex.Message}");
            }
        }


    }
}
