using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class Trigger
    {
        public enum triggerType
        {
            ammo
        }
        public double X { get; private set; }
        public double Y { get; private set; }
        public Collider collider { get; private set; }
        public triggerType type { get; private set; }
        public UIElement element { get; private set; }
        public Trigger(double x, double y, double width, double heigth, triggerType type, UIElement e)
        {
            X = x;
            Y = y;
            this.type = type;
            collider = new Collider(x, y, width, heigth);
            element = e;
        }
        public void Disappear()
        {
            element.Opacity = 0;
        }
    }
}
