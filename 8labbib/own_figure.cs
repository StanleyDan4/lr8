using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _8labib;

namespace _8labbib
{
    public class own_figure : Figure
    {
        public Rectagle rect1;
        public Rectagle rect2;
        public Ellipse c2;
        public Ellipse c3;
        public own_figure(int x, int y, int w, int h)
        {

            int centerX = x + w / 2;
            int centerY = y + h / 2;
            int circleSize = h / 4;
            int circleSiz = w / 4;
            int circleX = centerX - circleSiz / 2;

            this.c2 = new Ellipse(circleX - circleSiz, centerY + circleSize, circleSiz, circleSize);
            this.c3 = new Ellipse(circleX + circleSiz, centerY + circleSize, circleSiz, circleSize);
            this.rect2 = new Rectagle(x + w / 4, y, w / 2, h / 4);
            this.rect1 = new Rectagle(x, y + h/4, w, h/2);
        }
        public override void Draw()
        {
            this.c3.Draw();
            this.c2.Draw();
            this.rect1.Draw();
            this.rect2.Draw();
            Init.pictureBox.Image = Init.bitmap;
        }

        public override void MoveTo(int x, int y)
        {
            this.c2.x += x;
            this.c2.y += y;
            this.c3.x += x;
            this.c3.y += y;
            this.rect1.x += x;
            this.rect1.y += y;
            this.rect2.x += x;
            this.rect2.y += y;
            this.DeleteF(this, false);
            this.Draw();
        }
    }
}