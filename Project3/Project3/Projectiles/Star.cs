using Microsoft.Xna.Framework;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The star class
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Star : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Star" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        public Star(Vector2 position, Vector2 velocity)
        {
            Texture = Game1.textureDictionary["star"];
            Position = position;
            Velocity = velocity;
        }
    }
}