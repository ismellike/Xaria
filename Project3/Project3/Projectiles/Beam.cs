using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The Beam class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Beam : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Beam" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Beam(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["beam"];
            Damage = damage;
            Rectangle hitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }

        /* @Pre: beam projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the beam does
         * @Return: None
         */
        internal override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }
    }
}