using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    static class GameSettings
    {
        //Wymiary okna programu
        public static int window_width = 1024;
        public static int window_height = 768;

        //Wymiary i pozycja mapy na której będzie rysowana grafika
        public static int map_width = 480;
        public static int map_height = 480;
        public static int map_posX = 10;
        public static int map_posY = 10;

        //Czas w ms po którym następuje kolejna klatka
        public static int delta_frame_render = 15;

        //Wymiary jednej kafelki
        public static int tile_width = 32;
        public static int tile_height = 32;

        
    }
}
