using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KeyWatching.Host
{
	class Program
	{
		private const string Termination = "STOP IT";

		private static Queue<char> buffer = new Queue<char>();
		static void Main(string[] args)
		{
			using (var keyLogger = new EventedNativeKeyWatcher())
			{
				keyLogger.KeyLogged += Program.OnKeyLogged;
				Application.Run();
			}
		}

		private static void OnKeyLogged(object sender, KeyEventArgs e)
		{
			Console.Write(e.Key);
			Program.buffer.Enqueue(e.Key);

			while(Program.buffer.Count > Program.Termination.Length)
			{
				Program.buffer.Dequeue();
			}

			if(Program.buffer.Count == Program.Termination.Length)
			{
				var termination = new string(Program.buffer.ToArray());

				if(termination == Program.Termination)
				{
					Application.Exit();
				}
			}
		}
	}
}