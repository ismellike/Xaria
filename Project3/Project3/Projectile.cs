using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    /// <summary>
    /// The Projectile class
    /// </summary>
    class Projectile //can be an inherited class for different types of projectiles
    {
        public int Damage { get; internal set; }
        public Vector2 Position { get; internal set; }
        public Vector2 Velocity { get; internal set; }
        public Texture2D Texture { get; internal set; }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}