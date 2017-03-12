using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace PrismInteractionRequestTest.Views
{
	/// <summary>
	/// ダイアログWindow
	/// </summary>
	public partial class DialogWindow : Window
	{
		/// <summary>
		/// NativeMethods
		/// </summary>
		public abstract class SafeNativeMethods
		{
			[DllImport("user32.dll", EntryPoint = "GetWindowLong")]
			private static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

			[DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
			private  static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

			internal static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
			{
				if (IntPtr.Size == 8)
				{
					return GetWindowLongPtr64(hWnd, nIndex);
				}
				else
				{
					return GetWindowLongPtr32(hWnd, nIndex);
				}
			}

			[DllImport("user32.dll", EntryPoint = "SetWindowLong")]
			private static extern int SetWindowLong32(IntPtr hWnd, int nIndex, int dwNewLong);

			[DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
			private static extern IntPtr SetWindowLongPtr64(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

			internal static IntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, IntPtr dwNewLong)
			{
				if (IntPtr.Size == 8)
				{
					return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
				}
				else
				{
					return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
				}
			}

			[DllImport("user32.dll", SetLastError = true)]
			public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

			[DllImport("user32.dll", EntryPoint = "GetSystemMenu")]
			internal static extern IntPtr GetSystemMenu(IntPtr hwnd, int revert);

			[DllImport("user32.dll", EntryPoint = "GetMenuItemCount")]
			internal static extern int GetMenuItemCount(IntPtr hmenu);

			[DllImport("user32.dll")]
			internal static extern uint GetMenuItemID(IntPtr hMenu, int nPos);

			[DllImport("user32.dll", EntryPoint = "RemoveMenu")]
			internal static extern int RemoveMenu(IntPtr hmenu, int npos, int wflags);

			[DllImport("user32.dll", EntryPoint = "DrawMenuBar")]
			internal static extern int DrawMenuBar(IntPtr hwnd);
		}

		private const int GWL_EXSTYLE = -20;

		private const int WS_EX_DLGMODALFRAME = 0x0001;

		private const int SWP_NOSIZE = 0x0001;
		private const int SWP_NOMOVE = 0x0002;
		private const int SWP_NOZORDER = 0x0004;
		private const int SWP_FRAMECHANGED = 0x0020;

		private const uint SC_MOVE = 0xF010;
		private const uint SC_CLOSE = 0xF060;

		/// <summary>
		/// コンストラクタ
		/// </summary>
		public DialogWindow()
		{
			InitializeComponent();

			SourceInitialized += (s, e) =>
			{
				var hwnd = new WindowInteropHelper(this).Handle;

				// システムメニューアイコンを非表示にする
				IntPtr extendedStyle = SafeNativeMethods.GetWindowLongPtr(hwnd, GWL_EXSTYLE);
				SafeNativeMethods.SetWindowLongPtr(hwnd, GWL_EXSTYLE, new IntPtr(extendedStyle.ToInt64() | WS_EX_DLGMODALFRAME));
				SafeNativeMethods.SetWindowPos(hwnd, IntPtr.Zero, 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER | SWP_FRAMECHANGED);

				// システムメニューから「移動」「閉じる」以外を削除する
				IntPtr hmenu = SafeNativeMethods.GetSystemMenu(hwnd, 0);
				int count = SafeNativeMethods.GetMenuItemCount(hmenu);
				int index = 0;
				for (int i = 0; i < count; i++)
				{
					uint id = SafeNativeMethods.GetMenuItemID(hmenu, index);
					if (id != SC_CLOSE && id != SC_MOVE)
					{
						SafeNativeMethods.RemoveMenu(hmenu, (int)id, 0);
					}
					else
					{
						index++;
					}
				}
				SafeNativeMethods.DrawMenuBar(hwnd);
			};
		}
	}
}

