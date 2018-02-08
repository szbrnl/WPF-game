using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace WpfApplication1
{
    class GameInfo
    {
        private ScrollViewer gInfo;
        public GameInfo(ScrollViewer g)
        {
            gInfo = g;
        }
        public void Add(string s)
        {
            s = "\n" + DateTime.Now.TimeOfDay.ToString().Substring(0,8) + "> " + s;
            gInfo.Content += s;
            gInfo.ScrollToEnd();
        }
    }
}
