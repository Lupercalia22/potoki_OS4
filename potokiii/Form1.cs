using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace potokiii
{
	public partial class Form1 : Form
	{
		private Thread thread1;
		
		public Form1()
		{
			InitializeComponent();
			timer1.Enabled = true;
			Thread thread2 = new Thread((func))
			{
				Priority = ThreadPriority.Normal
			};
			thread2.Start();
			thread1 = new Thread(() =>
			{
				for (int i = 0; i < 10; i++)
				{
					richTextBox1.Text += $"Основной: {i}\n";
					Thread.Sleep(500);
				}
			})
			{
				Priority = ThreadPriority.Highest
			};
		}

		public int cnt = 0;
		public int i = 0;

		private void button1_MouseClick(object sender, MouseEventArgs e)
		{
			timer1.Enabled = true;
		
			cnt++;

			try
			{
				if (cnt == 1)
				{
					thread1.Start();
				}
				else
				{
					if (cnt %2 != 0)
					{
						thread1.Start();
					}
					else
					{
						thread1.Abort();
						//thread1.Join(500);
						thread1 = new Thread(() =>
						{
							for (int i = 0; i < 10; i++)
							{
								richTextBox1.Text += $"Основной: {i}\n";
								Thread.Sleep(500);
							}
						})
						{
							Priority = ThreadPriority.Highest
						};
					}
				}
			}
			catch(ThreadAbortException)
			{
				Thread.ResetAbort();
			}
		}
		
		void func()
		{
			while (timer1.Enabled)
			{
				richTextBox2.Text += $"Дочерний {i++}\n";
				Thread.Sleep(1000);
			}
			
		}
		

	}
}
