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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Project3.Screens
{
    class Start : Screen
    {
        public Start(ContentManager Content)
        {
            Buttons.Add(new Button(Content.Load<Texture2D>("button1"), new Vector2() {X = 500, Y = 500 }));
        }

        public override void Update(TouchLocation touch)
        {

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            
        }
    }
}