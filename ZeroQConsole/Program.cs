using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using ZMQ;
using Exception = System.Exception;

namespace ZeroQConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				// ZMQ Context
				using (Context context = new Context(1))
				{
					// Socket to talk to clients
					using (Socket socket = context.Socket(SocketType.REP))
					{
						socket.Bind("tcp://127.0.0.1:5555");
						Console.WriteLine("Listening on {0}", socket.Address);
						long counter = 0;
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();
						while (true)
						{
							// Wait for next request from client
							string message = socket.Recv(Encoding.Unicode);
							counter++;
							//Console.WriteLine("Received request: {0}", message);


							// Send reply back to client
							socket.Send(message + " World", Encoding.Unicode);
							if (counter % 2000 == 0) {
								Console.WriteLine("{0} messages per second", counter / stopwatch.Elapsed.TotalSeconds); 
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine(ex.StackTrace);

			}
			finally {
				Console.ReadLine();
			}
		}
	}
}
