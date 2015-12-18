using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int recv;
            byte[] data = new byte[1024];
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Any, 5566);
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Bind(ipEnd);
            socket.Listen(10);
            Console.Write("Waiting for a client");
            Socket client = socket.Accept();
            IPEndPoint ipEndClient = (IPEndPoint)client.RemoteEndPoint;
            Console.Write("Connect with {0} at port {1}", ipEndClient.Address, ipEndClient.Port);
            string welcome = "Welcome to my server";
            data = Encoding.ASCII.GetBytes(welcome);
            client.Send(data, data.Length, SocketFlags.None);
            while (true)
            {
                data = new byte[1024];
                recv = client.Receive(data);
                if (recv == 0)
                    break;
              
                Console.Write(Encoding.ASCII.GetString(data, 0, recv));
                 string ww = "hello";
                 data = Encoding.ASCII.GetBytes(ww);
                client.Send(data, 5, SocketFlags.None);
            }
            Console.Write("Disconnect form{0}", ipEndClient.Address);
            client.Close();
            socket.Close();
        }
    }
}
