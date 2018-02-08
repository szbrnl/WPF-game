using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfApplication1
{
    class KeyStates
    {
        public KeyStates()
        {
            _pressed.Clear();
        }
        private Dictionary<Key, bool> _pressed = new Dictionary<Key, bool>();
        public void Press(Key k)
        {
            _pressed[k] = true;
        }

        public void Release(Key k)
        {
            _pressed[k] = false;
        }

        public bool isPressed(Key k)
        {
            if (_pressed.ContainsKey(k))
                return _pressed[k];
            else return false;
        }



    }
  
}
