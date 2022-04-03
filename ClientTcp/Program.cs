using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientTcp
{
    class Program
    {
        static void Main(string[] args)
        {
            
            #region TCP
            const string ip = "127.0.0.1";
            const int port = 8080;

            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            Console.WriteLine("Введите сообщение:");
            var message = Console.ReadLine();

            var data = Encoding.UTF8.GetBytes(message);

            tcpSocket.Connect(tcpEndPoint);
            tcpSocket.Send(data);

            var buffer = new byte[256];
            var size = 0;
            var answer = new StringBuilder();

            do
            {
                size = tcpSocket.Receive(buffer);
                answer.Append(Encoding.UTF8.GetString(buffer, 0, size));
            }
            while (tcpSocket.Available > 0);

            Console.WriteLine(answer.ToString());

            tcpSocket.Shutdown(SocketShutdown.Both);
            tcpSocket.Close();

            Console.ReadLine();
            #endregion

            /*
            #region UDP
            const string _ip = "127.0.0.1";
            const int _port = 8082;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(_ip), _port);

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);

            while(true)
            {
                Console.WriteLine("Введите сообщение");
                var _message = Console.ReadLine();
                var serverEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081); 
                udpSocket.SendTo(Encoding.UTF8.GetBytes(_message), serverEndPoint);

                var _buffer = new byte[256];
                var _size = 0;
                var _data = new StringBuilder();
                EndPoint senderEndPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);

                do
                {

                    _size = udpSocket.ReceiveFrom(_buffer, ref senderEndPoint);
                    _data.Append(Encoding.UTF8.GetString(_buffer));
                }
                while (udpSocket.Available > 0);

                Console.WriteLine(_data);
                Console.ReadLine();
            }
            #endregion
            */
        }
    }
}
