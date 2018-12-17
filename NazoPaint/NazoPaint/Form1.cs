using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NazoPaint
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.MaximizeBox = false;
            SizeChange(new Size(1280, 720));
            this.bitmaps = new List<Bitmap>()
            {
                new Bitmap(1280,720)
            };
            this.EditIndex = 0;
            var g = Graphics.FromImage(this.bitmaps[0]);
            g.FillRectangle(Brushes.White, new Rectangle(0, 0, 1280, 720));
            g.Dispose();
            this.pictureBox1.Image = this.bitmaps[0];
            this.MouseOn = false;
            this.PaintColor = Color.Black;
            this.PaintSize = 5;
        }

        ~Form1()
        {
        }

        //ここから変数
        private Color PaintColor
        {
            get; set;
        }

        private float PaintSize
        {
            get;set;
        }

        private Point MouseLocation
        {
            get
            {
                return this.pictureBox1.PointToClient(MousePosition);
            }
        }

        private List<Bitmap> bitmaps;
        
        private int EditIndex
        {
            get; set;
        }

        private bool MouseOn
        {
            get; set;
        }

        private Point PrevPoint
        {
            get;set;
        }
        
        //変数ここまで
        
        private void PictureBoxMouseMove(object sender, MouseEventArgs e)
        {
            var loc = this.MouseLocation;
            this.toolStripStatusLabel2.Text = $"({loc.X}, {loc.Y})";
            if (this.MouseOn)
            {
                var g = Graphics.FromImage(this.bitmaps[this.EditIndex]);
                var b = new SolidBrush(this.PaintColor);
                var p = new Pen(b, this.PaintSize);
                g.DrawLine(p, this.PrevPoint, loc);
                g.FillEllipse(b, loc.X - this.PaintSize / 2, loc.Y - this.PaintSize / 2, this.PaintSize, this.PaintSize);
                g.FillEllipse(b, this.PrevPoint.X - this.PaintSize / 2, this.PrevPoint.Y - this.PaintSize / 2, this.PaintSize, this.PaintSize);
                b.Dispose();
                g.Dispose();
                this.pictureBox1.Invalidate();
            }
            this.PrevPoint = loc;
        }

        private void PictureBoxMouseLeave(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "";
            this.MouseOn = false;
        }

        private void SizeChange(Size to)
        {
            var s = this.pictureBox1.Size;
            var diff = to - s;
            this.Size = this.Size + diff;
            this.pictureBox1.Size = to;
        }

        private void ColorChange(Color color)
        {
            this.PaintColor = color;
        }

        private void SizeChange(float size)
        {
            this.PaintSize = size;
        }

        private void BlackButtonClick(object sender, EventArgs e)
        {
            ColorChange(this.blackButton.BackColor);
        }

        private void WhiteButtonClick(object sender, EventArgs e)
        {
            ColorChange(this.whiteButton.BackColor);
        }

        private void RedButtonClick(object sender, EventArgs e)
        {
            ColorChange(this.redButton.BackColor);
        }

        private void BlueButtonClick(object sender, EventArgs e)
        {
            ColorChange(this.blueButton.BackColor);
        }

        private void YellowButtonClick(object sender, EventArgs e)
        {
            ColorChange(this.yellowButton.BackColor);
        }

        private void LimeButtonClick(object sender, EventArgs e)
        {
            ColorChange(this.limeButton.BackColor);
        }

        private void PictureBox1MouseDown(object sender, MouseEventArgs e)
        {
            this.MouseOn = true;
        }

        private void PictureBox1MouseUp(object sender, MouseEventArgs e)
        {
            this.MouseOn = false;
        }

        private void PaintSizeValueChanged(object sender, EventArgs e)
        {
            SizeChange((float)this.numericUpDown1.Value);
        }

        private void NewFileClick(object sender, EventArgs e)
        {

        }
    }
}
