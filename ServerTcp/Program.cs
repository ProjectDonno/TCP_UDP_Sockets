using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Project18
{
    class Program
    {
        static void Main(string[] args)
        {
            
            #region TCP
            const string ip = "127.0.0.1";
            const int port = 8080;

            // точка, к которой мы будем обращаться
            var tcpEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            // сокет, который мы будем использовать
            var tcpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            // связывает сокет с точкой
            tcpSocket.Bind(tcpEndPoint);
            // очередь "слушателей" состоит из 5 слотов
            tcpSocket.Listen(5);

            while(true)
            {
                // создаётся для каждого нового клиента
                var listener = tcpSocket.Accept();
                // место хранения полученных данных, максимум можем получить сообщение из 256 байт
                var buffer = new byte[256];
                // количество байт, реально полученных из сообщения
                var size = 0;
                var data = new StringBuilder();

                do
                {
                    size = listener.Receive(buffer);
                    data.Append(Encoding.UTF8.GetString(buffer, 0, size));
                }
                while (listener.Available > 0);

                Console.WriteLine(data);

                listener.Send(Encoding.UTF8.GetBytes("Успех"));

                // выключили
                listener.Shutdown(SocketShutdown.Both);
                // закрыли
                listener.Close();
            }
            #endregion

            /*
            #region UDP
            const string _ip = "127.0.0.1";
            const int _port = 8081;

            var udpEndPoint = new IPEndPoint(IPAddress.Parse(_ip),_port);

            var udpSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            udpSocket.Bind(udpEndPoint);
            
            while(true)
            {

            
            var buffer = new byte[256];
            var size = 0;
            var data = new StringBuilder();
            EndPoint senderEndPoint = new IPEndPoint(IPAddress.Any, 0);

            do
            {

                size = udpSocket.ReceiveFrom(buffer, ref senderEndPoint);
                data.Append(Encoding.UTF8.GetString(buffer));
            }
            while (udpSocket.Available > 0);

            udpSocket.SendTo(Encoding.UTF8.GetBytes("Сообщение получено"),senderEndPoint);

            Console.WriteLine(data);
            }
            // TODO: закрытие сокета
            #endregion
            */
        }
    }
}
