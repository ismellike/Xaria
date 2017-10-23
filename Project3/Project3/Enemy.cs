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

namespace Project3
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    class Enemy 
    {
        public Texture2D Texture { get; internal set; }
        public Vector2 Position { get; internal set; }
        public int Health { get; internal set; }

        /// <summary>
        /// Draws character texture to position with no tint
        /// </summary>
        /// <param name="spriteBatch"></param>
        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public virtual void Shoot() { } //can be inherited by enemies to shoot projectiles down
    }
}