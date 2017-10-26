using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Project3
{
    class Screen
    {
        public List<Button> Buttons = new List<Button>();
        public virtual void Update(TouchLocation touch) { }
        public virtual void Draw(SpriteBatch spriteBatch) { }
    }
}