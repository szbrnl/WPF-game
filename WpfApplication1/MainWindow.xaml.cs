using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;

namespace WpfApplication1
{
    public partial class MainWindow : Window
    {

        private KeyStates kStates = new KeyStates();
        Game game;
        BackgroundWorker _backgroundWorker;

        public MainWindow()
        {
            InitializeComponent();
            game = new Game(this, kStates);

            this.KeyUp += MainWindow_KeyUp;
            this.KeyDown += MainWindow_KeyDown;

          

            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += _backgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += _backgroundWorker_RunWorkerCompleted;

            Update();  
        }

        private void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            Thread.Sleep(GameSettings.delta_frame_render);
        }

        //Wywołuje się co 15ms
        private void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (!game.isGameOver)
            {
                game.NextFrame();
                Update();
            }
        }

        public void Update()
        {
            _backgroundWorker.RunWorkerAsync();
            
        }

        public void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {}

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.A)
            {
                kStates.Press(e.Key);
            }
            else if (e.Key == Key.S)
            {
                kStates.Press(e.Key);
            }
            else if (e.Key == Key.D)
            {
                kStates.Press(e.Key);
            }
            else if (e.Key == Key.W)
            {
                kStates.Press(e.Key);
            }
            else if (e.Key == Key.E)
            {
                kStates.Press(e.Key);
            }
            else if (e.Key == Key.Space)
            {
                kStates.Press(e.Key);
            }
            else if (e.Key == Key.Q)
            {
                kStates.Press(e.Key);
            }
        }

        private void MainWindow_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.A)
            {
                kStates.Release(e.Key);
            }
            else if (e.Key == Key.S)
            {
                kStates.Release(e.Key);
            }
            else if (e.Key == Key.D)
            {
                kStates.Release(e.Key);
            }
            else if (e.Key == Key.W)
            {
                kStates.Release(e.Key);
            }
            else if (e.Key == Key.E)
            {
                kStates.Release(e.Key);
            }
            else if (e.Key == Key.Space)
            {
                kStates.Release(e.Key);
            }
            else if (e.Key == Key.Q)
            {
                kStates.Release(e.Key);
            }
        }

      
        
    }
}

