using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8labbib
{
    public class Ellipse : Figure
    {
        public static readonly string createellipse;

        public Operator op { get; set; }
        public string createllipse;
        public Ellipse( int x, int y, int w, int h)
        {
            this.x = x;
            this.y = y;
            this.w = w;
            this.h = h;
           
        }
        public Ellipse()
        {
            x = 0;
            y = 0;
            w = 0;
            h = 0;

        }
        public override void Izm(int w)
        {
            
        }
        public override void Draw()
        {
            Graphics g1 = Graphics.FromImage(Init.bitmap);
            g1.DrawEllipse(Init.pen, x, y, w, w);

            Init.pictureBox.Image = Init.bitmap;
        }

        public override void MoveTo(int x, int y)
        {
            if (!((this.x + x < 0 && this.y + y < 0)||(this.y + y < 0)||
                  (this.x + x > Init.pictureBox.Width && this.y + y < 0)||
                  (this.x + this.w + x > Init.pictureBox.Width)||
                  (this.x + x > Init.pictureBox.Width && this.y + y > Init.pictureBox.Height)||
                  (this.y + this.h + y > Init.pictureBox.Height)||
                  (this.x + x < 0 && this.y + y > Init.pictureBox.Height) || (this.x + x < 0)))
            {

                this.x += x;
                this.y += y;
                DeleteF(this, false);
                Draw();
            }
        }
    }
}
