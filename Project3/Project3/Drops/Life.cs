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
using Microsoft.Xna.Framework;

namespace Xaria.Drops
{
    class Life : Drop
    {

        public Life(Vector2 position)
        {
            Position = position;
            Texture = Game1.textureDictionary["life"];
        }

        public override void OnReceive(ref Player player)
        {
            player.ExtraLives++;
        }
    }
}