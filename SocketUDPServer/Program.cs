using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.Net.Sockets;
using System.Threading;
namespace SocketUDPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            int recv;
            int port = 1514;
            string host = "127.0.0.1";
            byte[] recvBytes = new byte[1024];
            //创建终结点
            IPAddress ip = IPAddress.Parse(host);
            IPEndPoint ipe = new IPEndPoint(ip, port);

            Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            s.Bind(ipe);
           
            Console.WriteLine("等待客户端连接");

            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 0);
            EndPoint remote = (EndPoint)(sender);
            Console.WriteLine("建立连接");
            recv = s.ReceiveFrom(recvBytes, ref remote);
            Console.WriteLine("Message  recevie from {0}",remote.ToString());
            string str = System.Text.Encoding.ASCII.GetString(recvBytes, 0, recv);
            Console.WriteLine("Messsage:{0}",str);
            str = "Hello Client!";

            recvBytes = System.Text.Encoding.ASCII.GetBytes(str);
            //Thread.Sleep(1000);  
            //发送到客户端
            s.SendTo(recvBytes,remote);
           
            Console.ReadLine();
        }
    }
}
