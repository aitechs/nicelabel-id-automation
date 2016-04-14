using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IDAutomation
{
	public partial class Form1 : Form
	{
        NicelabelAutomation.IdGenerator _Generator;

        public Form1()
		{
			InitializeComponent();
            
		}

		private void button1_Click(object sender, EventArgs e)
		{
            _Generator = new NicelabelAutomation.IdGenerator(@"School ID 2016.lbl");
            _Generator.Print(cboPrinter.Text);
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			foreach (string printer in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
			{
				cboPrinter.Items.Add(printer);
			}
		}

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_Generator != null) _Generator.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateThumbnail();
        }

        protected void GenerateThumbnail()
        {
            string path = @"C:\_SQL Database\promis\Pictures\students\2015-2016\1603080460.jpg";
            System.Drawing.Image image = System.Drawing.Image.FromFile(path);
            using (System.Drawing.Image thumbnail = image.GetThumbnailImage(200, 200, new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback), IntPtr.Zero))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    thumbnail.Save(memoryStream, ImageFormat.Png);
                    Byte[] bytes = new Byte[memoryStream.Length];
                    memoryStream.Position = 0;
                    memoryStream.Read(bytes, 0, (int)bytes.Length);
                    string base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                    pictureBox1.ImageLocation ="data:image/png;base64," + base64String;
                    pictureBox1.Visible = true;
                }
            }
        }

        public bool ThumbnailCallback()
        {
            return false;
        }
    }
}
