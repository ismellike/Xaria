using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    /// <summary>
    /// The Projectile class
    /// </summary>
    class Projectile : GameElement//can be an inherited class for different types of projectiles
    {
        public int Damage { get; internal set; }
        public Vector2 Velocity { get; internal set; }

        internal void Update()
        {
            Position += Velocity;
        }
    }
}