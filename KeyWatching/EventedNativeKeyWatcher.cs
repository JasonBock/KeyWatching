using System;

namespace KeyWatching
{
	public sealed class EventedNativeKeyWatcher
		: NativeKeyWatcher
	{
		public event EventHandler<KeyEventArgs> KeyLogged;

		protected override void HandleKey(int keyCode)
		{
			if (keyCode >= char.MinValue && keyCode <= char.MaxValue)
			{
				this.KeyLogged?.Invoke(
					this, new KeyEventArgs(Convert.ToChar(keyCode)));
			}
		}
	}
}
