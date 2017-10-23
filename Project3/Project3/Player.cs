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
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    /// <summary>
    /// Our player class
    /// </summary>
    class Player
    {
        public int Health { get; internal set; }
        public Texture2D Texture { get; internal set; }
        public Vector2 Position { get; internal set; }

        public List<Projectile> Projectiles = new List<Projectile>();

        public Player(int health)
        {
            Health = health;
            Texture = Game1.textureDictionary["Player"];
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}