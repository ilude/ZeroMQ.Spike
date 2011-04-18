using System;
using System.Text;
using System.Threading;
using ZMQ;

namespace ZeroQClient
{
	class Program
	{
		static void Main(string[] args)
		{
			//  Prepare our context and socket
			Context context = new Context(1);
			Socket socket = context.Socket(SocketType.REQ);
			socket.Connect("tcp://localhost:5555");

			long counter = 0;
			while(true) {
				socket.Send(string.Format("{0, 20:n} Hello", ++counter), Encoding.Unicode);
				//  Wait for next request from client
				var message = socket.Recv(Encoding.Unicode);
				Console.WriteLine(message);
				Thread.Sleep(50);
			}
		}
	}
}
