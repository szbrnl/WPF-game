using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    class Collider
    {
        public double X { get; private set; }
        public double Y { get; private set; }
        public double height { get; private set; }
        public double width { get; private set; }

        public Collider(double  x, double y, double w, double h)
        {
            X = x+2;
            Y = y+2;
            width = w;
            height = h;
        }

        public void Set(double x, double y, double w, double h)
        {
            X = x +2;
            Y = y +2;
            width = w;
            height = h;
        }
        //Sprawdza czy dwa collidery nie nachodzą na siebie
        public bool CheckCollision(Collider c)
        {
            if(c.Y+c.height <= Y)
                return false;
            if (c.Y >= Y + height)
                return false;
            if (c.X + c.width <= X)
                return false;
            if (c.X >= X + width)
                return false;

            return true;
            
        }
        
    }
}
