using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows;
namespace WpfApplication1
{
    class Location
    {
        public bool isFinished { get; private set; } = false;
        public bool isBowAvailable { get; private set; } = false;

        public string levelName { get; private set; }
        public Thickness playerPosition { get; private set; }
        public Thickness startingPoint { get; private set; }
        public int enemiesLeft { get; private set; } = 0;
        
        private Canvas canvas;
        private GameInfo gInfo;
        private Level level;

        private List<string> files = new List<string>();
        private List<int> zIndex = new List<int>();
        private List<Collider> map_colliders = new List<Collider>();
        private List<Collider> exit;
        private List<Enemy> enem = new List<Enemy>();
        private List<Boss> boss = new List<Boss>();
        private List<Bullet> bullets = new List<Bullet>();
        private List<FireBullet> fBullets = new List<FireBullet>();
        private List<Trigger> triggers = new List<Trigger>();

        private CroppedBitmap[] tiles = new CroppedBitmap[256];
        

        public Location(Canvas canv, Level l, GameInfo gInf)
        {
            canvas = canv;
            gInfo = gInf;
            Load(l);
        }

        public void Load(Level l)
        {
            level = l;
            isBowAvailable = level.bow;
            startingPoint = new Thickness(level.playerStart.Left * 32, level.playerStart.Top * 32, 0, 0);
            enemiesLeft = level.enemyPaths.Count + level.bossPaths.Count;
            levelName = level.levelName;
            exit = level.exitColliders;
            DrawLayers();
            LoadColliders();
            AddEnemies();
        }

        public void EnemyShoot(FireBullet b)
        {
            fBullets.Add(b);
        }

        public void PlayerShoot(Bullet b)
        {
            bullets.Add(b);
        }

        //Aktualizacja położenia wszystkich poruszających się przedmiotów
        public void UpdateMovingObjects(Player player)
        {
            //Strzały gracza
            int i = 0;
            while(i<bullets.Count)
            {
                if(bullets[i].ticksToDisappear==0)
                {
                    bullets[i].NextFrame();
                    bullets.RemoveAt(i);
                }
                else if(CheckMapCollisions(bullets[i].collider))
                {
                    bullets[i].NextFrame();
                    triggers.Add(new Trigger(bullets[i].X, bullets[i].Y, 5, 5, Trigger.triggerType.ammo,bullets[i].rect));
                    bullets.RemoveAt(i);
                    
                }
                else
                {
                    bool hit = false;
                    foreach (Enemy e in enem)
                    {
                        if (e.characterCollider.CheckCollision(bullets[i].collider))
                        {
                            e.Hit();
                            triggers.Add(new Trigger(bullets[i].X, bullets[i].Y, 5, 5, Trigger.triggerType.ammo,bullets[i].rect));
                            bullets.RemoveAt(i);
                            hit = true;
                            break;
                        }
                    }
                    if (!hit)
                    {
                        bullets[i].NextFrame();
                        i++;
                    }
                }
            }
            //Strzały przeciwników -czerwone
            i = 0;
            while(i<fBullets.Count)
            {
                if(fBullets[i].ticksToDisappear==0)
                {
                    fBullets[i].NextFrame();
                    fBullets.RemoveAt(i);
                }            
                else if(player.characterCollider.CheckCollision(fBullets[i].collider))
                {
                    player.HitByEnemy();
                    fBullets[i].Disappear();
                    fBullets.RemoveAt(i);
                }
                else
                {
                    fBullets[i].NextFrame();
                    i++;
                }
            }
        }

        //Aktualizacja wszystkiego - co klatkę
        public void NextFrame(Player player)
        {
            playerPosition = player.image.Margin;
            UpdateMovingObjects(player);
            EnemiesPlay(player);
            CheckTriggers(player);
        }

        //Sprawdzanie czy trigger został aktywowany
        private void CheckTriggers(Player player)
        {
            int i = 0;
            while(i<triggers.Count)
            {
                if (player.characterCollider.CheckCollision(triggers[i].collider))
                {
                    player.arrows++;
                    
                    triggers[i].Disappear();
                    triggers.RemoveAt(i);
                }
                else
                    i++;
            }
        }

        //Wyświetlanie elementów mapy
        private void DrawLayers()
        {
            GetTiles();
            for(int i=0; i<level.filesToLoad.Count; i++)
            {
                string[] lines = File.ReadAllLines("../../assets/maps/" +levelName+ level.filesToLoad[i]);
                int x = 0, y = 0;
                foreach (string sq in lines)
                {
                    string[] ss = sq.Split(',');
                    foreach (string a in ss)
                    {
                        if (a != "-1")
                        {
                            Image img = new Image();
                            img.Height = 32;
                            img.Width = 32;
                            img.Source = tiles[Convert.ToInt32(a)];
                            img.Margin = new Thickness(x * 32, y * 32, 0, 0);
                            img.SetValue(Canvas.ZIndexProperty, level.zIndex[i]);
                            canvas.Children.Add(img);
                        }
                        x++;
                    }


                    x = 0;
                    y++;
                }
            }
        }

        //Dodanie wszystkich wrogów i bossów przewidzianych w lokacji
        private void AddEnemies()
        {
            foreach (List<Thickness> t in level.enemyPaths)
            {
                    Image enemy = new Image();
                    enemy.Width = 40;
                    enemy.Height = 40;
                    canvas.Children.Add(enemy);
                if (level.tickToShoot == 0)
                    enem.Add(new Enemy(enemy, canvas, new Thickness(t[0].Left, t[0].Top, 0, 0), this, t));
                else
                    enem.Add(new ShootingEnemy(enemy, canvas, new Thickness(t[0].Left, t[0].Top, 0, 0), this, t, level.tickToShoot));
            }
            foreach (List<Thickness> t in level.bossPaths)
            {
                Image enemy = new Image();
                enemy.Width = 120;
                enemy.Height = 120;
                canvas.Children.Add(enemy);
                enem.Add(new Boss(enemy, canvas, new Thickness(t[0].Left, t[0].Top, 0, 0), this, t, level.tickToShoot));
            }

        }

        //Sprawdzanie czy wyjście jest w zasięgu gracza
        private bool IsExitInRange(Player player)
        {
            foreach (Collider e in exit)
            {
                if (e.CheckCollision(player.characterCollider))
                    return true;
            }
            return false;

        }

        //Sprawdzanie czy wyjście z lokacji jest możliwe
        public void TryExit(Player player)
        {
            if (enemiesLeft == -1)
            {
                if (IsExitInRange(player))
                    isFinished = true;
            }
            else
                gInfo.Add("Musisz pokonać wszystkich wrogów w tej lokacji");
        }

        //Poruszanie się przeciwników
        public void EnemiesPlay(Player pl)
        {
            if(enemiesLeft == 0)
            {
                gInfo.Add("Można przejść dalej");
                enemiesLeft--;
            }
            for(int i=0; i<enem.Count; i++)
            {
                if (enem[i].hp == 0)
                {
                    enem[i].Dispose();
                    enem.Remove(enem[i]);
                    enemiesLeft--;
                }
                else
                {
                    enem[i].AI();
                }
            }
            
            if (CheckEnemyCollision(pl.characterCollider) && !pl.untouchable)
                pl.HitByEnemy();
        }
     
        //Resetowanie lokacji przed wczytaniem następnej
        public void Reset()
        {
            canvas.Children.RemoveRange(2, canvas.Children.Count-2);
            files.Clear();
            zIndex.Clear();
            boss.Clear();
            exit.Clear();
            bullets.Clear();
            fBullets.Clear();
            enem.Clear();
            triggers.Clear();
            map_colliders.Clear();
            isFinished = false;
            enemiesLeft = 1;
        }
        
        //Sprawdzanie czy gracz nie wyszedł poza mapę
        public bool CheckMapBorderCollision(Collider c)
        {
            foreach (Collider mm in map_colliders.GetRange(0, 4))
            {
                if (c.CheckCollision(mm)) return true;
            }

            return false;
        }

        //Sprawdzanie kolizji obiektu z colliderami na mapie
        public bool CheckMapCollisions(Collider c)
        {
            //Kolizje z obiektami na mapie
            foreach (Collider mm in map_colliders.GetRange(4, map_colliders.Count - 4))
            {
                if (c.CheckCollision(mm)) return true;
            }

            return false;
        }        

        //Sprawdzanie kolizji gracza z przeciwnikami
        public bool CheckEnemyCollision(Collider c)
        {
            //Kolizje z przeciwnikami
            foreach (Enemy e in enem)
            {
                if (c.CheckCollision(e.characterCollider))
                {
                    return true;
                }
                if (e.blink)
                    e.Blink();
            }

            return false;
        }

        //Sprawdzanie czy jakiś wróg jest w zasięgu ataku i ewentualne zaatakowanie
        public void AttackEnemy(Collider c)
        {
            foreach (Enemy e in enem)
            {
                if (c.CheckCollision(e.characterCollider))
                {
                    e.Hit();
                }
            }
        }

        //Wycinanie kafelek
        private void GetTiles()
        {
            BitmapImage tileset = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(canvas), "assets/maps/"+level.spritesheet));

            int count = 0;
            for (int i = 0; i < 16; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    CroppedBitmap cb = new CroppedBitmap(tileset, new Int32Rect(j * 32, i * 32, 32, 32));
                    tiles[count++] = cb;
                }
            }
        }

        //Tworzenie colliderów, brzegi mapy i przeszkody
        private void LoadColliders()
        {
            map_colliders.Add(new Collider(0, 0, GameSettings.map_width, 1));
            map_colliders.Add(new Collider(0, 0, 1, GameSettings.map_height));
            map_colliders.Add(new Collider(GameSettings.map_width, 0, 1, GameSettings.map_posY + GameSettings.map_height));
            map_colliders.Add(new Collider(0, GameSettings.map_height, GameSettings.map_width, 1));

            string[] lines = File.ReadAllLines("../../assets/maps/"+levelName+level.filesToLoad[level.filesToLoad.Count-1]);
            int x = 0, y = 0;
            foreach (string a in lines)
            {
                string[] ss = a.Split(',');
                foreach (string b in ss)
                {
                    if (b != "-1")
                    {
                        map_colliders.Add(new Collider(x * 32+6, y * 32+6, GameSettings.tile_width-12, GameSettings.tile_width-12));
                    }
                    x++;
                }


                x = 0;
                y++;
            }
        }

    }
}
