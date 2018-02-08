using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    class Enemy : Character, IDisposable
    {
        public int hp { get; protected set; } = 100;

        //Miejsca miedzy ktorymi porusza sie przeciwnik
        private Thickness startingPoint;
        private Thickness endingPoint;

        //Czy mruga
        public bool blink { get; private set; } = false;
        
        private int currentPath = 1;
        private List<Thickness> path;

        double distance;

        public Enemy(Image img, Canvas canv, Thickness characterPosition, Location loc, List<Thickness> path) : base(img, canv, characterPosition, loc)
        {
            this.path = path;
            loadImage("/assets/character/idle_2.png");
            Render();

            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height-5);
            startingPoint = characterPosition;
            endingPoint = new Thickness(path[1].Left, path[1].Top, 0, 0);
            
            distance = Math.Sqrt(Math.Pow(position.Left - endingPoint.Left, 2) + Math.Pow(position.Top - endingPoint.Top, 2));
        }

        //Poruszanie się między punktami z listy
        protected void AutoMove()
        {
            double dist = Math.Sqrt(Math.Pow(position.Left - endingPoint.Left, 2) + Math.Pow(position.Top - endingPoint.Top, 2));
            if (dist >= 1)
            {
                double dX = (endingPoint.Left - position.Left) / dist;
                double dY = (endingPoint.Top - position.Top) / dist;
                Move(dX, dY);

            }
            else
            {
                MoveTo(endingPoint.Left, endingPoint.Top);
                startingPoint = endingPoint;

                if (currentPath < path.Count)
                    endingPoint = path[currentPath++];
                else
                {

                    currentPath = 0;
                    endingPoint = path[0];
                }
            }
        }

        public virtual void AI()
        {
            AutoMove();
        }

        protected void MoveTo(double x, double y)
        {
            position.Left = x;
            position.Top = y;
            image.Margin = position;
            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height);
        }

        protected virtual void Move(double x, double y)
        {
            position.Left += x;
            position.Top += y;
            image.Margin = position;
            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height);
        }

        public void Hit()
        {
            hp -= 50;
            blink = true;
        }

        private int blinkTime = 5;
        public void Blink()
        {

            if (blinkTime > 0)
            {
                blink = true;
                image.Opacity = 0.3;
                blinkTime--;
            }
            
            else
            {
                image.Opacity = 1;
                blinkTime = 5;
                blink = false;
            }
        }

        public override void Move(dir q)
        {
            Thickness tmpPos = position;
            switch (q)
            {
                case dir.up:
                    position.Top -= speed;
                    facing = dir.up;
                    break;
                case dir.down:
                    position.Top += speed;
                    facing = dir.down;
                    break;
                case dir.right:
                    position.Left += speed;
                    facing = dir.right;
                    break;
                case dir.left:
                    position.Left -= speed;
                    facing = dir.left;
                    break;
            }
                     

            Render();
            image.Margin = position;
            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height);

        }

        public void Dispose()
        {
            image.Source = null;
        }
    }
}
