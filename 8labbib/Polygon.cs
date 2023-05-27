using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8labbib
{
    public class Polygon : Figure
    {
        public PointF[] pointFs;
        public int count;
        public Polygon(int count)
        {
            this.pointFs = new PointF[count];

        }
        public override void MoveTo(int x, int y)
        {
            int i = 0;
            if (!((pointFs[0].X + y < 0)
                || (pointFs[0].X + x > Init.pictureBox.Width && pointFs[0].Y + y < 0)
                || (pointFs[0].X + x > Init.pictureBox.Width)
                || (pointFs[0].X + x > Init.pictureBox.Width && pointFs[0].Y + y > Init.pictureBox.Height)
                || (pointFs[0].Y + y > Init.pictureBox.Height)
                || (pointFs[0].X + x < 0 && pointFs[0].Y + y > Init.pictureBox.Height)
                || (pointFs[0].X + x < 0) || (pointFs[1].X + y < 0)
                || (pointFs[1].X + x > Init.pictureBox.Width && pointFs[1].Y + y < 0)
                || (pointFs[1].X + x > Init.pictureBox.Width)
                || (pointFs[1].X + x > Init.pictureBox.Width && pointFs[1].Y + y > Init.pictureBox.Height)
                || (pointFs[1].Y + y > Init.pictureBox.Height)
                || (pointFs[1].X + x < 0 && pointFs[1].Y + y > Init.pictureBox.Height)
                || (pointFs[1].X + x < 0)))
            {
                foreach (PointF pointF in pointFs)
                {
                    pointFs[i].X += x;
                    pointFs[i].Y += y;
                    i++;
                }
                DeleteF(this, false);
                Draw();
            }
        }

        public override void Draw()
        {
            Graphics g = Graphics.FromImage(Init.bitmap);
            g.DrawPolygon(Init.pen, this.pointFs);
            Init.pictureBox.Image = Init.bitmap;

        }
        public override void Izm(int w)
        {
            
        }


    }
}
