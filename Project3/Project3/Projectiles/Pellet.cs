using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The rocket class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Pellet : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Laser" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Pellet(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["pellet"];
            Damage = damage;
            ProjectileType = Type.Pellet;
        }

        /* @Pre: pellet projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the pellet does
         * @Return: None
         */
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }

        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}