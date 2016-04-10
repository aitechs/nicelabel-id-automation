using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDAutomation
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			using (NicelabelAutomation.IdGenerator generator = new NicelabelAutomation.IdGenerator("d:\\testLabel.lbl"))
			{
				generator.Print(cboPrinter.Text);
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				cboPrinter.Items.Add(printer);
			}
		}
	}
}
