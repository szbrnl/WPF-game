using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    class Bullet : UIElement
    {
        public double X { get; protected set; }
        public double Y { get; protected set; }
        public Vector direction { get; protected set; }
        public Rectangle rect { get; protected set; }
        public Collider collider { get; protected set; }
        public int ticksToDisappear { get; protected set; }

        protected Canvas canvas;
        protected int bulletSpeed = 5;

        public Bullet(double x, double y, Vector v, Canvas canvas, int TTD = 1000)
        {
            X = x;
            Y = y;
            direction = v;
            this.canvas = canvas;
            collider = new Collider(X, Y, 5, 5);
            ticksToDisappear = TTD;

            rect = new Rectangle();
            rect.Width = 5;
            rect.Height = 5;
            rect.Margin = new Thickness(x, y, 0, 0);
            rect.SetValue(Canvas.ZIndexProperty, 3);

            SolidColorBrush mySolidColorBrush = new SolidColorBrush(Color.FromArgb(255, 255, 255, 0));
            rect.Fill = mySolidColorBrush;

            canvas.Children.Add(rect);
        }

        //Aktualizacja położenia co klatkę
        public void NextFrame()
        {
            if (X > GameSettings.map_width || X <= 0 || Y > GameSettings.map_height || Y <= 0)
            {
                rect.Opacity = 0;
                ticksToDisappear = 0;
            }
            else if(ticksToDisappear>0)
            {
                ticksToDisappear--;
                X += direction.X * bulletSpeed;
                Y += direction.Y * bulletSpeed;
                rect.Margin = new Thickness(X, Y, 0, 0);
                collider.Set(X, Y, 5, 5);
            }
            else
            {
                canvas.Children.Remove(rect);
            }
        }

    }
}
