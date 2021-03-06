﻿using System;
using System.Collections.Immutable;

namespace KeyWatching
{
	public sealed class BufferedKeysEventArgs
		: EventArgs
	{
		public BufferedKeysEventArgs(ImmutableArray<char> keys)
			: base() => this.Keys = keys;

		public ImmutableArray<char> Keys { get; }
	}
}