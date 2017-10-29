using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Our player class
    /// </summary>
    class Player : GameElement
    {
        public int Health { get; internal set; }
        public List<Projectile> Projectiles = new List<Projectile>();

        public Player(int health)
        {
            Health = health;
            Texture = Game1.textureDictionary["ship"];
            Position = new Vector2() { X = Game1.screenSize.X / 2f, Y = Game1.screenSize.Y - Texture.Height - 10 };
        }

        internal void Update(bool left)
        {
            if(left)
            {
                Position.X--;
            }
            else
            {
                Position.X++;
            }
        }
    }
}