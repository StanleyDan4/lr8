using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _8labbib
{
    public class ShapeContainer
    {
        public static List<Figure> figurelist = new List<Figure>();

        public static void AddFigure(Figure figure)
        {
            figurelist.Add(figure);
        }
        public static Figure FindFigure(string name)
        {
            foreach (Figure figure in figurelist)
            {
                if (figure.name == name)
                {
                    return figure;
                }
            }
            return null;
        }
    }
}

