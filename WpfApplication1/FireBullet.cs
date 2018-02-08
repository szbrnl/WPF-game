using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace WpfApplication1
{
    class FireBullet : Bullet
    {
        
        public FireBullet(double x, double y, Vector v, Canvas canvas, int TTD = 100) : base(x, y, v, canvas, TTD)
        {

            SolidColorBrush mySolidColorBrush = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
            rect.Fill = mySolidColorBrush;
            bulletSpeed = 3;
            
        }
        public void Disappear()
        {
            rect.Opacity = 0;
        }
    }
}
