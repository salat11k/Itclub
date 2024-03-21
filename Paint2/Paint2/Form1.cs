using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint2
{
    public partial class Form1 : Form
    {
        private Paint _paint;
        public Form1()
        {
            InitializeComponent();

            _paint = new Paint(pictureBox1);
            panel1.BackColor = Color.Black;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                _paint.ChangeColor(colorDialog1.Color);
                panel1.BackColor = colorDialog1.Color;
            }
        }
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            _paint.ChangeBrushWidth(numericUpDown1.Value);
        }
        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            _paint.ChangeTool(((RadioButton)sender).Name);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            _paint.Clear();
        }
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            _paint.OnMouseDown(e.Location);
        }
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            _paint.OnMouseMove(e.Location);
        }
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            _paint.OnMouseUp(e.Location);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = "MyImage";
            saveFileDialog1.DefaultExt = "jpg";
            saveFileDialog1.Filter = "JPEG FILES(*.jpg)|*.jpg";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _paint.SaveImage(saveFileDialog1.FileName);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "MyImage";
            openFileDialog1.DefaultExt = "jpg";
            openFileDialog1.Filter = "JPEG FILES(*.jpg)|*.jpg";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _paint.OpenImage(saveFileDialog1.FileName);
            }
        }
    }
}
