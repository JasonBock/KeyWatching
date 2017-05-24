using System;
using System.Collections.Generic;
using static System.Collections.Immutable.ImmutableArray;

namespace KeyWatching
{
	public sealed class BufferedEventedNativeKeyWatcher
		: NativeKeyWatcher
	{
		public event EventHandler<BufferedKeysEventArgs> KeysLogged;

		private readonly ushort size;
		private List<char> buffer;

		public BufferedEventedNativeKeyWatcher(ushort size)
		{
			this.buffer = new List<char>(size);
			this.size = size;
		}

		protected override void HandleKey(int keyCode)
		{
			if (keyCode >= Char.MinValue && keyCode <= char.MaxValue)
			{
				this.buffer.Add(Convert.ToChar(keyCode));

				if (this.buffer.Count >= this.size)
				{
					this.KeysLogged?.Invoke(
						this, new BufferedKeysEventArgs(this.buffer.ToImmutableArray()));
					this.buffer = new List<char>(this.size);
				}
			}
		}
	}
}