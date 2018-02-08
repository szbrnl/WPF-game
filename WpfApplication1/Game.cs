using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1
{
    class Game
    {
        private Level[] levels = new Level[] { Level.level_1, Level.level_2, Level.level_3, Level.level_4};

        public bool isGameOver { get; private set; } = false;
        private MainWindow mainWindow;
        private Player player;
        private KeyStates kStates;
        private GameInfo gInfo;
        private Location currentLevel;
        private int levelNumber = 0;

        public Game(MainWindow win, KeyStates kS)
        {
            mainWindow = win;
            
            kStates = kS;
            gInfo = new GameInfo(mainWindow.game_info);
            currentLevel = new Location(mainWindow.canvas, levels[levelNumber], gInfo);

            if(levels[levelNumber].messageOnEnter!="")
                gInfo.Add(levels[levelNumber].messageOnEnter);

            mainWindow.ammo.Opacity = 0;
            mainWindow.endGame.Opacity = 0;

            player = new Player(win.character, mainWindow.canvas, currentLevel.startingPoint, currentLevel);
        }

        private void UpdateUI()
        {
            mainWindow.hp.Content = "HP: "+player.hp;
            mainWindow.ammo.Content = "Amunicja: "+player.arrows;
        }

        public void NextFrame()
        {
            if (player.hp>0)
            {
                if (currentLevel.isFinished)
                {
                    if (levels[levelNumber].messageOnExit != "")
                        gInfo.Add(levels[levelNumber].messageOnExit);
                    levelNumber++;
                   
                    ChangeLevel();
                }
                
                currentLevel.NextFrame(player);
                player.NextFrame(kStates);
                
                UpdateUI();
            }
            else
            {
                GameOver(false);
            }
        }

        private void ChangeLevel()
        {
            if (levels.Length == levelNumber)
                GameOver(true);
            else
            {
                gInfo.Add("Ukończyłeś poziom " + levelNumber);
                currentLevel.Reset();
                
                currentLevel.Load(levels[levelNumber]);

                if (currentLevel.isBowAvailable)
                    mainWindow.ammo.Opacity = 1;

                gInfo.Add("Poziom " + (levelNumber+1));
                player.SetPosition(currentLevel.startingPoint);

                if (levels[levelNumber].messageOnEnter != "")
                    gInfo.Add(levels[levelNumber].messageOnEnter);

            }
        }

        private void GameOver(bool win)
        {
            if(win)
            {
                mainWindow.endGame.Content = "Wygrałeś!";
                mainWindow.endGame.Opacity = 1;
                gInfo.Add("Zwycięstwo!");
                isGameOver = true;
            }
            else
            {
                mainWindow.endGame.Content = "Przegrałeś!";
                mainWindow.endGame.Opacity = 1;
                gInfo.Add("Nie żyjesz");
                isGameOver = true;
                
            }
        }
    }
}

