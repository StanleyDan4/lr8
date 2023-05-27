using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using _8labbib;
using _8labib;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _8labbibf
{
    public partial class Form3 : Form
    {
        private int numPoints;
        Triangle triangle;
        private int i = 0;
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (int.Parse(textBox1.Text) <= Init.pictureBox.Width && int.Parse(textBox2.Text) <= Init.pictureBox.Height)
                {
                    if (i == 0)
                    {
                        this.triangle = new Triangle(3);
                    }
                    triangle.pointFs[i].X = int.Parse(textBox1.Text);
                    triangle.pointFs[i].Y = int.Parse(textBox2.Text);
                    i++;
                }
                if (i == 3)
                {
                    button2.Enabled = true;
                    button1.Enabled = false;
                }
            }
            catch
            {
                MessageBox.Show("Неверно! Введите данные повторно");
            }
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            triangle.Draw();
            ShapeContainer.AddFigure(triangle);
        }
    }
}
