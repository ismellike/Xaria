using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The rocket class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Rocket : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Laser" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Rocket(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            //Texture = Game1.textureDictionary["Rocket"];
            Damage = damage;
        }

        internal override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            //spriteBatch.
        }
    }
}