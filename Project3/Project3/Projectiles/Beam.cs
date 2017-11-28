using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The Beam class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Beam : Projectile
    {
        public const int DEFAULT_DMG = 100;
        /// <summary>
        /// Initializes a new instance of the <see cref="Beam" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Beam(Vector2 position, Vector2 velocity, int damage, bool immovable = true)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["beam"];
            Damage = damage;
            Immovable = immovable;
            ProjectileType = Type.Beam;
        }

        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            Draw(ref spriteBatch, Color.Red);
        }

        /* @Pre: beam projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the beam does
         * @Return: None
         */
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }

        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            Enemies[y][x].Damage(Damage);
        }
    }
}