using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The Laser class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Laser : Projectile
    {
        /// <summary>
        /// The default DMG
        /// </summary>
        public const int DEFAULT_DMG = 50;
        /// <summary>
        /// Initializes a new instance of the <see cref="Laser" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Laser(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["laser"];
            Damage = damage;
            ProjectileType = Type.Laser;
        }

        /// <summary>
        /// Draws from enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            Draw(ref spriteBatch, Color.Red);
        }

        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="Enemies">The enemies.</param>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            Enemies[y][x].Damage(Damage);
        }

        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }
    }
}