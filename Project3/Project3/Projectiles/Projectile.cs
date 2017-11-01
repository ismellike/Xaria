using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    /// <summary>
    /// The Projectile class
    /// </summary>
    public class Projectile : GameElement//can be an inherited class for different types of projectiles
    {
        public int Damage { get; internal set; }
        public Vector2 Velocity { get; internal set; }
        //Rectangle hitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
    }
}