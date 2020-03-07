using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ConsoleApplication2
{
    class Square
    {
        public Point ponto;        
        public bool life;

        public Square(Point p, bool lifeIN)
        {
            ponto = p;
            life = lifeIN;
        }
    }
}
