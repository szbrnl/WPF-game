using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    class Boss : ShootingEnemy
    {
        private int numberOfBullets = 20;
        public Boss(Image img, Canvas canv, Thickness characterPosition, Location loc, List<Thickness> path, int ticksToShoot) : base(img, canv, characterPosition, loc, path, ticksToShoot)
        {
            loadImage("/assets/character/dragon.png");
            image.Width = 120;
            image.Height = 120;
            image.SetValue(Canvas.ZIndexProperty, 10);
            characterCollider.Set(position.Left + 20, position.Top + 40, image.Width - 30, image.Height - 65);
            this.speed = 0.4;
            hp = 300;
            Render();
        }

        protected override void Shoot()
        {
            if (idleTicks == 0)
            {
                originX = position.Left + image.Width / 2;
                originY = position.Top + image.Height / 2;
                Vector vec = new Vector(0,1);
                
                for(int i=0; i<numberOfBullets; i++)
                {
                    location.EnemyShoot(new FireBullet(originX, originY, vec, canvas));
                    double x = vec.X*Math.Cos(2*Math.PI/numberOfBullets) - vec.Y * Math.Sin(2*Math.PI/numberOfBullets);
                    double y = vec.X* Math.Sin(2*Math.PI / numberOfBullets) + vec.Y * Math.Cos(2*Math.PI / numberOfBullets);
                    vec.X = x;
                    vec.Y = y;
                    Console.WriteLine(i);
                }
                idleTicks = ticksToShoot;
            }
            else
                idleTicks--;
        }

        protected override void Move(double x, double y)
        {
            position.Left += x;
            position.Top += y;
            image.Margin = position;
            characterCollider.Set(position.Left + 20, position.Top + 40, image.Width - 30, image.Height - 65);
        }

        public override void AI()
        {
            base.AI();
            Shoot();
        }
    }
}
