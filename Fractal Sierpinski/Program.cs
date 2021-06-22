using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Fractal_Sierpinski
{
	class Program
	{
		[System.Runtime.InteropServices.DllImport("user32.dll",
		CharSet = System.Runtime.InteropServices.CharSet.Auto,
		CallingConvention =
			System.Runtime.InteropServices.CallingConvention.StdCall)]
		public static extern void mouse_event(uint dwFlags,
		int dx,
		int dy,
		int dwData,
		int dwExtraInfo);

		//Нормированные абсолютные координаты
		private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

		//Нажатие на левую кнопку мыши
		private const int MOUSEEVENTF_LEFTDOWN = 0x0002;

		//Поднятие левой кнопки мыши
		private const int MOUSEEVENTF_LEFTUP = 0x0004;

		//перемещение указателя мыши
		private const int MOUSEEVENTF_MOVE = 0x0001;
		static void Click(int X, int Y)
		{
			mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, X, Y, 0, 0);
			mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
			mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
		}
		[DllImport("user32.dll")]
		public static extern int GetAsyncKeyState(Int32 i);

		static void Main(string[] args)
		{
			Console.WriteLine("\nPress a key to display; press the 'x' key to quit.");
			//int[] Tx = { 852 * 65535 / 1366, 983 * 65535 / 1366, 1109 * 65535 / 1366 };
			//int[] Ty = { 550 * 65535 / 768, 294 * 65535 / 768, 550 * 65535 / 768 };
			int[] Tx = { 559 * 65535 / 1366, 931 * 65535 / 1366, 1302 * 65535 / 1366 }; // Bbb.
			int[] Ty = { 649 * 65535 / 768, 195 * 65535 / 768, 649 * 65535 / 768 }; // Bbb.

			int x = Tx[0], y = Ty[0];
			System.Threading.Thread.Sleep(2000);
			Random random = new Random();
			const int Count = 50000;
			for (int j = 0; j < Count; j++)
			{
				for (Int32 i = 0; i < 255; i++)
				{
					int state = GetAsyncKeyState(i);
					if (state == 1 || state == -32767)
					{
						if ((Keys)i == Keys.Escape)
						{
							j = Count;
						}
						//Console.WriteLine((Keys)i);
					}
				}
				System.Threading.Thread.Sleep(50);
				var dot = random.Next(3);
				x = (x + Tx[dot]) / 2;
				y = (y + Ty[dot]) / 2;
				Click(x, y);
			}
			//Console.ReadKey();
		}
	}
}
