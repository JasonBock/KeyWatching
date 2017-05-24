using System;
using System.Windows.Forms;

namespace KeyWatching
{
	public abstract class KeyWatcher
		: IDisposable
	{
		protected KeyWatcher()
			: base() { }

		public abstract void Dispose();

		protected abstract void HandleKey(int keyCode);
	}
}
