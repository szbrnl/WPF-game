using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Navigation;

namespace WpfApplication1
{
    abstract class Character
    {
        public Collider characterCollider { get; private set; }
        public Image image { get; private set; }
        protected Thickness position;

        protected ImageBrush sprite = new ImageBrush();
        protected Canvas canvas;
        protected double speed = 2;
        protected Location location;
        protected dir facing = dir.right;

        public enum dir
        {
            up, down, right, left
        }

        public Character(Image img, Canvas canv, Thickness characterPosition, Location loc)
        {
            canvas = canv;
            
            image = img;
            image.SetValue(Canvas.ZIndexProperty, 4);
            image.Height = 32;
            image.Width = 32;
            
            position = characterPosition;
            characterCollider = new Collider(characterPosition.Left, characterPosition.Top, image.Width, image.Height);
            location = loc;
 
            Render();        
        }

        

        protected void loadImage(string s)
        {
            sprite.ImageSource = new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(canvas), s));
        }
        protected void Render()
        {
            //  -> odwracanie postaci
          image.Source = sprite.ImageSource;
          image.Margin = position;

        }
        public void SetPosition(Thickness t)
        {
            position = t;
            characterCollider.Set(position.Left, position.Top, image.Width, image.Height);
            image.Margin = position;
        }
        public abstract void Move(dir q);
        
        
        
    }
}
