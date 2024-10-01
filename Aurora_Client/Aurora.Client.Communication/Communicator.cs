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

        private async Task<ResponseInfo> ReadMessageFromServer()
        {
            var stream = _client.GetStream();
            var headerMessage = new byte[HEADER_SIZE];
            await stream.ReadAsync(headerMessage, 0, HEADER_SIZE);
            var messageSize = BitConverter.ToInt32(headerMessage, 1);
            var wholeMessage = new byte[HEADER_SIZE + messageSize];
            Array.Copy(headerMessage, wholeMessage, HEADER_SIZE);
            await stream.ReadAsync(wholeMessage, HEADER_SIZE, messageSize);
            ResponseInfo response;
            response.message = Encoding.UTF8.GetString(wholeMessage, HEADER_SIZE, messageSize);
            response.code = (ResponseCode)wholeMessage[0];
            return response;
        }



        public async void ConnectToServerAsync()
        {
            try
            {
                await _client.ConnectAsync(CONNECTION_IP, CONNECTION_PORT);
                RequestInfo requestInfo;
                requestInfo.code = RequestCode.SIGN_UP_REQUEST_CODE;
                requestInfo.message = "dodido###123456";
                await SendMessageToServer(requestInfo);
                ResponseInfo info = await ReadMessageFromServer();
                await TokenManager.Instance.SaveTokenToFileAsync(info.message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Communication could not be established due to {ex.Message}");
            }
        }


    }
}
