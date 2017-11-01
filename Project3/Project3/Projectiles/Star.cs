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
using Microsoft.Xna.Framework;

namespace Project3.Projectiles
{
    class Star : Projectile
    {
        public Star(Vector2 position, Vector2 velocity)
        {
            Texture = Game1.textureDictionary["star"];
            Position = position;
            Velocity = velocity;
            Damage = 100;
        }
    }
}