using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WpfApplication1
{
    class Level
    {
        public int levelNumber { get; private set; }
        public string levelName { get; private set; }
        public string spritesheet { get; private set; }
        public string messageOnEnter { get; private set; }
        public string messageOnExit { get; private set; }
        public List<string> filesToLoad { get; private set; }
        public List<int> zIndex { get; private set; }
        public Thickness playerStart { get; private set; }
        public List<Collider> exitColliders { get; private set; }
        public List<List<Thickness>> enemyPaths { get; private set; }
        public bool bow { get; private set; }
        public int tickToShoot { get; private set; }
        public List<List<Thickness>> bossPaths { get; private set; }

        public Level(int number, string levelName, string spritesheet, string onEnter, string onExit, bool bow, 
            List<string> filesToLoad, List<int> zIndex, Thickness startingPoint, List<Collider> exitColliders, 
            List<List<Thickness>> enemyPaths, List<List<Thickness>> bossPath, int tickToShoot)
        {
            levelNumber = number;
            this.levelName = levelName;
            this.spritesheet = spritesheet;
            messageOnEnter = onEnter;
            messageOnExit = onExit;
            this.bow = bow;
            this.filesToLoad = filesToLoad;
            this.zIndex = zIndex;
            playerStart = startingPoint;
            this.exitColliders = exitColliders;
            this.enemyPaths = enemyPaths;
            this.tickToShoot = tickToShoot;
            this.bossPaths = bossPath;
        }


        public static Level level_1 =
           new Level(1, "map1", "map1.png", "Witaj! Sterujesz na wsadzie, atakujesz spacją w kierunku w którym \n\twskazujesz kursorem.\n\tAby przejść dalej zabij wszystkich wrogów, podejdź do krawędzi mapy\n\ti naciśnij E. Zerkaj tu czasami, bo będą się tu pojawiać wszystkie informacje. \n\tPowodzenia!"
               , "", false,
               new List<string>() { "_ground.csv", "_tree0.csv", "_tree1.csv", "_colliders.csv" },      //pliki collider
               new List<int>() { 1, 5, 6, 2 },                                                          //zIndex
               new Thickness(7, 13, 0, 0),                                                              //Start
               new List<Collider>()                                                                     //Wyjście
               {
                    new Collider(14*32, 5 * 32, 32, 32),
                    new Collider(14*32, 6 * 32, 32, 32),
                    new Collider(14*32, 7 * 32, 32, 32)
               },
               new List<List<Thickness>>()
                   {
                        new List<Thickness>()                                                           //Ścieżki przeciwników
                        {
                            new Thickness(32*10, 32*8, 0, 0),
                            new Thickness(32*7, 32*8, 0, 0),
                            new Thickness(32*8,32*10,0,0)
                        }
                   },
               new List<List<Thickness>>()
               , 0);

        public static Level level_2 =
           new Level(1, "map2", "map1.png", "Pokonaj wrogów aby przejść dalej.", "Otrzymujesz łuk! Naciśnij Q aby strzelić.\n\tMożesz podnosić amunicję z ziemi", false,
               new List<string>() { "_ground.csv", "_ground2.csv",
                   "_tree4.csv", "_tree3.csv",  "_tree2.csv", "_colls.csv" },      //pliki collider
               new List<int>() { 0, 1, 5, 6, 7, 3 },                                                          //zIndex
               new Thickness(0, 6, 0, 0),                                                              //Start
               new List<Collider>()                                                                     //Wyjście
               {
                    new Collider(14*32, 10 * 32, 32, 32),
                    new Collider(14*32, 11 * 32, 32, 32),
                    new Collider(14*32, 12 * 32, 32, 32),
                    new Collider(14*32, 13 * 32, 32, 32),
                    new Collider(14*32, 14 * 32, 32, 32)
               },
               new List<List<Thickness>>()
                   {
                        new List<Thickness>()                                                           //Ścieżki przeciwników
                        {
                            new Thickness(32*10, 32*8, 0, 0),
                            new Thickness(32*7, 32*8, 0, 0),
                            new Thickness(32*8,32*10,0,0)
                        }
                   },
               new List<List<Thickness>>(),
               0);

        public static Level level_3 =
          new Level(1, "map3", "map1.png", "", "", true,
              new List<string>() { "_ground.csv", "_ground1.csv",
                   "_tree2.csv", "_tree1.csv",  "_tree.csv", "_colls.csv" },      //pliki collider
              new List<int>() { 0, 1, 5, 6, 7, 3 },                                                          //zIndex
              new Thickness(0, 11, 0, 0),                                                              //Start
              new List<Collider>()                                                                     //Wyjście
              {
                    new Collider(14*32, 2 * 32, 32, 32),
                    new Collider(14*32, 3 * 32, 32, 32),
                    new Collider(14*32, 4 * 32, 32, 32),
                    new Collider(14*32, 5 * 32, 32, 32),
              },
              new List<List<Thickness>>()
                  {
                        new List<Thickness>()                                                           //Ścieżki przeciwników
                        {
                            new Thickness(32*0, 32*3, 0, 0),
                            new Thickness(32*4, 32*7, 0, 0),
                            new Thickness(32*9,32*5,0,0),
                            new Thickness(32*14,32*4,0,0),
                            new Thickness(32*9,32*5,0,0),
                            new Thickness(32*4, 32*7, 0, 0)
                        },
                        new List<Thickness>()
                        {
                            new Thickness(0,32*2,0,0),
                            new Thickness(32*10,32*4,0,0),
                            new Thickness(32*12,32*1,0,0)
                        }

                  },
              new List<List<Thickness>>(),
              120);

        public static Level level_4 =
          new Level(1, "map4", "mountain_landscape.png", "Pokonaj smoka aby ukończyć grę!", "", true,
              new List<string>() { "_ground.csv", "_tree.csv",
                   "_tree1.csv", "_tree2.csv",  "_tree3.csv", "_colls.csv" },      //pliki collider
              new List<int>() { 0, 5, 6, 7, 8, 3 },                                                          //zIndex
              new Thickness(0, 5, 0, 0),                                                              //Start
              new List<Collider>()                                                                     //Wyjście
              {
                    new Collider(4*32, 0 * 32, 32, 32),
                    new Collider(5*32, 0 * 32, 32, 32),
                    new Collider(6*32, 0 * 32, 32, 32),
                    new Collider(7*32, 0 * 32, 32, 32),
              },
              new List<List<Thickness>>(),
              new List<List<Thickness>>()
                   {
                        new List<Thickness>()                                                           //Ścieżki przeciwników
                        {
                            new Thickness(32*7, 32*0, 0, 0),
                            new Thickness(32*10, 32*0, 0, 0),
                            new Thickness(32*11,32*12,0,0)
                        }
                   }, 200);
    }

}
