using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ZMQ;

namespace ZeroQStressClient
{
	class Program
	{
		private const int Max = 10;
		private const string Message = "Hello";
		private const string Output = "{0, 20:n} {1, 20} {2}";

		static void Main(string[] args) {
			var threads = new Thread[Max];

			for (int i = 0; i < Max; i++) {
				threads[i] = new Thread(Runner);
				threads[i].Name = string.Format("thread {0}", i);
				threads[i].Start();
			}

			threads[Max - 1].Join();
		}

		public static void Runner() {
			//  Prepare our context and socket
			Context context = new Context(1);
			Socket socket = context.Socket(SocketType.REQ);
			socket.Connect("tcp://localhost:5555");

			long counter = 0;
			while (true)
			{
				socket.Send(Message, Encoding.Unicode);
				//  Wait for next request from client
				var message = socket.Recv(Encoding.Unicode);
				//Console.WriteLine(Output, ++counter, Thread.CurrentThread.Name, message);
				//Thread.Sleep(5);
			}
		}
	}
}
