using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Neiron
{
	public partial class Form1 : Form
	{
		private List<Point> _list = new List<Point>();
		private bool _flagPaint = false;
		private int  _sizeData = 16;
		private NeironService neironService;
		public Form1()
		{
			InitializeComponent();
			ConvertImage.Clear(pictureBox1, _sizeData, _sizeData);
			neironService = new NeironService();
		}


		private void paint_box_MouseDown(object sender, MouseEventArgs e)
		{
			_flagPaint = true;
			_list.Add(new Point(e.X * _sizeData / pictureBox1.Width, e.Y * _sizeData / pictureBox1.Height));
		}

		private void paint_box_MouseMove(object sender, MouseEventArgs e)
		{
			if (_flagPaint)
			{
				_list.Add(new Point(e.X * _sizeData / pictureBox1.Width, e.Y * _sizeData / pictureBox1.Height));
				Bitmap image = (Bitmap)pictureBox1.Image;
				using (Graphics g = Graphics.FromImage(image))
				{
					g.DrawLines(new Pen(Color.Black), _list.ToArray());
				}
				pictureBox1.Image = image;

			}
		}

		private void paint_box_MouseUp(object sender, MouseEventArgs e)
		{
			_flagPaint = false;
			_list = new List<Point>();
		}

		private void paint_box_MouseLeave(object sender, EventArgs e)
		{
			_flagPaint = false;
			_list = new List<Point>();
		}

		private void button1_Click(object sender, EventArgs e)
		{
			double[,] data = ConvertImage.GetImageData((Bitmap)pictureBox1.Image, _sizeData);
			var result = neironService.GetCategory(data);
			textBox2.Text = result;
		}

		private void button2_Click(object sender, EventArgs e)
		{
			ConvertImage.Clear(pictureBox1, _sizeData, _sizeData);
		}

		private void button3_Click(object sender, EventArgs e)
		{
			comboBox1.Items.Add(textBox1.Text);
		}

		private void button5_Click(object sender, EventArgs e)
		{
			comboBox1.Items.Clear();
		}

		private void button4_Click(object sender, EventArgs e)
		{
			var categories = new List<string>();
			foreach (var item in comboBox1.Items)
			{
				categories.Add(item.ToString());
			}
			neironService.Init(categories);
		}
	}
}
