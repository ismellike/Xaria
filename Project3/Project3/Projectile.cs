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

        public Projectile(Vector2 position, Vector2 velocity)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["projectile"];
        }
    }
}