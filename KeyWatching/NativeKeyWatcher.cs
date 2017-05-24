using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace KeyWatching
{
	public abstract class NativeKeyWatcher
		: KeyWatcher
	{
		// Reference: https://blogs.msdn.microsoft.com/toub/2006/05/03/low-level-keyboard-hook-in-c/
		private bool isDisposed;
		private readonly IntPtr hookId;
		private Native.LowLevelKeyboardProc keyboardProc;

		public NativeKeyWatcher()
			: base()
		{
			IntPtr KeyboardProcessor(int nCode, IntPtr wParam, IntPtr lParam)
			{
				if (nCode >= 0 && wParam == (IntPtr)Native.WM_KEYDOWN)
				{
					var keyCode = Marshal.ReadInt32(lParam);
					this.HandleKey(keyCode);
				}

				return Native.CallNextHookEx(this.hookId, nCode, wParam, lParam);
			}

			IntPtr SetHook()
			{
				this.keyboardProc = new Native.LowLevelKeyboardProc(KeyboardProcessor);

				using (var process = Process.GetCurrentProcess())
				{
					using (var module = process.MainModule)
					{
						return Native.SetWindowsHookEx(Native.WH_KEYBOARD_LL,
							this.keyboardProc,
							Native.GetModuleHandle(module.ModuleName), 0);
					}
				}
			}

			this.hookId = SetHook();
		}

		~NativeKeyWatcher() => this.Dispose(false);

		public override void Dispose() => this.Dispose(true);

		protected virtual void Dispose(bool disposing)
		{
			if (!this.isDisposed)
			{
				Native.UnhookWindowsHookEx(this.hookId);
				this.keyboardProc = null;
				this.isDisposed = true;
			}
		}
	}
}