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
    class Laser : Projectile
    {
        public Laser(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["laser"];
            Damage = 1000;
            Rectangle hitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}