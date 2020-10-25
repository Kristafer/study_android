using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab6
{
    public partial class Form1 : Form
    {
        private readonly Algorithm algorithm;
        private readonly Random rand;
        public Form1()
        {
            InitializeComponent();
            algorithm = new Algorithm();
            rand = new Random();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int count = int.Parse(textBox1.Text);
            dataGridView.ColumnCount = count + 1;
            dataGridView.RowCount = count + 1;
            var data = new double[count, count];

            for (int i = 1; i <= count; i++)
            {
                dataGridView[0, i].Value = dataGridView[i, 0].Value = $"X{i}";
            }


            for (int i = 0; i < count; i++)
            {
                for (int j = 0; j < count; j++)
                {
                    data[i, j] = i==j? 0: Math.Round(rand.NextDouble()*10+1,1);

                    dataGridView[i+1, j+1].Value = data[i, j];
                }
            }

            algorithm.SetData(count, data);
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
