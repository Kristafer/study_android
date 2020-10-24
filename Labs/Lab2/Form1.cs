using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab1
{
    public partial class Form1 : Form
    {
        private Graphics graphics;
        private BufferedGraphics bufferedGraphics;
        private Maximin kMeans;
        private bool isSetValue;

        public Form1()
        {
            InitializeComponent();
            graphics = displayBox.CreateGraphics();
            bufferedGraphics = new BufferedGraphicsContext().Allocate(graphics, new Rectangle(0, 0, displayBox.Width, displayBox.Height));
            kMeans = new Maximin(bufferedGraphics);
            isSetValue = false;
        }

        private void set_Click(object sender, EventArgs e)
        {
            kMeans.KInit(int.Parse(textBox1.Text), displayBox.Width, displayBox.Height);
            isSetValue = true;
        }

        private void clear_Click(object sender, EventArgs e)
        {
            kMeans.ClearBox();
            isSetValue = false;
        }

        private void kmeans_Click(object sender, EventArgs e)
        {
            kMeans.MaxMinDo();
        }
    }
}
