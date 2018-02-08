using System; 
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace WpfApplication1
{
    class Player : Character
    {
        private double mouseX; 
        private double mouseY; 


        public int arrows { get; set; } = 10;
        public int hp { get; private set; } = 100;
        private Rectangle rect;
        public bool untouchable { get; private set; } = false;
        public Player(Image img, Canvas canv, Thickness characterPosition, Location loc) : base(img, canv, characterPosition, loc)
        {
            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height-12);
            loadImage("/assets/character/idle_1.png");
            Render();
            rect = new Rectangle();
            rect.Width = 2;
            rect.Height = 2;
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromRgb(102, 255, 102);
            rect.Fill = mySolidColorBrush;
            rect.SetValue(Canvas.ZIndexProperty, 50);
            canvas.Children.Insert(1,rect);
            DrawCrosshair();
        }
        
        private void DrawCrosshair()
        {                    
            double px = position.Left + image.Width / 2;
            double py = position.Top + image.Height / 2;
            Vector vec = new Vector(mouseX - px, mouseY - py);
            vec.Normalize();
            vec *= 30;

            rect.Margin = new Thickness(px + vec.X, py + vec.Y, 0, 0);
        }

        public void HandleInput(KeyStates kStates)
        {
            if (kStates.isPressed(Key.A))
                Move(Character.dir.left);
            if (kStates.isPressed(Key.S))
               Move(Character.dir.down);
            if (kStates.isPressed(Key.W))
                Move(Character.dir.up);
            if (kStates.isPressed(Key.D))
                Move(Character.dir.right);
            if (kStates.isPressed(Key.Space))
            {
                kStates.Release(Key.Space);
                Attack();
            }
            if(kStates.isPressed(Key.Q))
            {
                kStates.Release(Key.Q);
                if (location.isBowAvailable && arrows > 0)
                {
                    Shoot();
                    arrows--;
                }
            }
            if(kStates.isPressed(Key.E))
            {
                kStates.Release(Key.E);
                location.TryExit(this);
            }

            mouseX = Mouse.GetPosition(null).X - GameSettings.map_posX;
            mouseY = Mouse.GetPosition(null).Y - GameSettings.map_posY;
            DrawCrosshair();
            



        }
        public void HitByEnemy()
        {
            untouchable = true;
            hp -= 10;
        }

        private void Shoot()
        {
           
            double px = position.Left + image.Width / 2;
            double py = position.Top + image.Height / 2;
            Vector vec = new Vector(mouseX - px, mouseY - py);
            vec.Normalize();
            Bullet b = new Bullet(px, py, vec, canvas);

            location.PlayerShoot(b);

        }

        private void Attack()
        {
           
            double px = position.Left + image.Width / 4;
            double py = position.Top + image.Height / 4;
            Vector vec = new Vector(mouseX - px, mouseY - py);
            vec.Normalize();
            vec *= 30;

            Collider att = new Collider(px + vec.X, py + vec.Y, 10, 10);

            location.AttackEnemy(att);
        }

        public void NextFrame(KeyStates kStates)
        {
            HandleInput(kStates);
            if (untouchable)
                Blink();
        }

        private int blinkTime = 6;
        private int blinkCount = 10;
        public void Blink()
        {
            
            if(blinkTime>0)
            {
                blinkTime--;
            }
            else if(blinkCount==1)
            {
                blinkCount = 10;
                blinkTime = 6;
                untouchable = false;
                image.Opacity = 1;
            }
            else
            {
                if (image.Opacity == 0.3)
                    image.Opacity = 1;
                else
                    image.Opacity = 0.3;
                blinkTime = 6;
                blinkCount--;
            }
        }
        public override void Move(dir q)
        {
            //+Sprawdzanie czy nie wychodzi poza mape, nie wchodzi w przeszkody, nie powoduje kolizji
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

            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height);

            if (location.CheckMapCollisions(characterCollider) || location.CheckMapBorderCollision(characterCollider))
            {
                position = tmpPos;
                characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height);
            }
            if (location.CheckEnemyCollision(characterCollider))
            {
                
                if(!untouchable)
                    HitByEnemy();
                

            }

            Render();
            image.Margin = position;
            characterCollider.Set(position.Left + 6, position.Top + 6, image.Width - 12, image.Height);

        }
    }
}
