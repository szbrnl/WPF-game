using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    class ShootingEnemy : Enemy
    {
        public int idleTicks { get; protected set; }
        protected int ticksToShoot;
        protected double originX;
        protected double originY;

        public ShootingEnemy(Image img, Canvas canv, Thickness characterPosition, Location loc, List<Thickness> path, int ticksToShoot) : base(img, canv, characterPosition, loc, path)
        {
            loadImage("/assets/character/idle_2.png");
            Render();
            idleTicks = ticksToShoot;
            this.ticksToShoot = ticksToShoot;

            originX = characterPosition.Left + img.Width / 2;
            originY = characterPosition.Top + img.Height / 2;
        }

        protected virtual void Shoot()
        {
            originX = position.Left + image.Width / 2;
            originY = position.Top + image.Height / 2;
            if (idleTicks == 0)
            {
                Vector vec = new Vector(location.playerPosition.Left - originX, location.playerPosition.Top - originY);
                vec.Normalize();
                location.EnemyShoot(new FireBullet(position.Left, position.Top, vec, canvas));
                idleTicks = ticksToShoot;
            }
            else
                idleTicks--;
        }
        public override void AI()
        {
            base.AI();
            Shoot();
        }
    }
}
