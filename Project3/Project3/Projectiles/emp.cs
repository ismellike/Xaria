using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The Emp class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Emp : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Laser" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Emp(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["Emp"];
            Damage = damage;
           // Rectangle hitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); Given by this.Bounds();
        }

        internal override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            Enemies[y][x].Health -= Damage;
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