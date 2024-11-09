using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Aurora.Server.Communication
{
    public class Communicator
    {
        private static Communicator _instance;
        private Dictionary<TcpClient, IRequestHandler> _clients;
        private List<LoggedUser> _loggedUsers;
        private TcpListener _server;

        private int SERVER_LISTEN_PORT = 1223;
        private int BUFFER_SIZE = 1024;
        private static readonly int CODE_AMOUNT_BYTES = 1;
        private static readonly int BYTES_LENGTH = 4;
        private static readonly int HEADER_SIZE = CODE_AMOUNT_BYTES + BYTES_LENGTH;

        public Communicator()
        {
            _clients = new Dictionary<TcpClient, IRequestHandler>();
            _server = new TcpListener(IPAddress.Any, SERVER_LISTEN_PORT);
            _server.Start();
        }

        public static Communicator Instance
        {
            get
            {
                _instance ??= new Communicator();
                return _instance;  
            }
        }

        public void AcceptClients()
        {
            while (true)
            {
                _clients.Add(_server.AcceptTcpClient(), RequestHandlerFactory.Instance.GetJWTRequestHandler());
                _ = Task.Run(() => { _ = HandleClientAsync(_clients.Last().Key, _clients.Last().Value); });
            }
        }

        private async Task<RequestInfo> ReadMessage(NetworkStream stream)
        {
            var header = new byte[HEADER_SIZE];
            await stream.ReadAsync(header, 0, HEADER_SIZE);
            var length = BitConverter.ToInt32(header, CODE_AMOUNT_BYTES);
            var wholeMessage = new byte[length + HEADER_SIZE];
            Array.Copy(header, wholeMessage, HEADER_SIZE);
            await stream.ReadAsync(wholeMessage, HEADER_SIZE, length);
            RequestInfo info;
            info.code = (RequestCode)wholeMessage[0];
            info.data = Encoding.UTF8.GetString(wholeMessage, HEADER_SIZE, wholeMessage.Length - HEADER_SIZE);
            return info;
        }

        private async Task SendMessage(NetworkStream stream, ResponseInfo result)
        {
            var wholeMessage = new byte[HEADER_SIZE + result.message.Length];
            wholeMessage[0] = (byte)result.code;
            byte[] arr = BitConverter.GetBytes(result.message.Length);
            Array.Copy(arr, 0, wholeMessage, 1, 4);
            Array.Copy(Encoding.UTF8.GetBytes(result.message), 0, wholeMessage, HEADER_SIZE, result.message.Length);
            await stream.WriteAsync(wholeMessage);
        }

        private async Task HandleClientAsync(TcpClient client, IRequestHandler handler)
        {
            while (true)
            {
                ResponseInfo result;
                RequestInfo info = await ReadMessage(client.GetStream());
                if (handler.IsRequestValid(info))
                {
                    (handler, result) = await handler.HandleRequest(info);
                    await SendMessage(client.GetStream(), result);
                }
                else
                {
                    throw new Exception("not working");
                }
            } 
        }
    }
}