using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The Beam projectile class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Beam : Projectile
    {
        /// <summary>
        /// The default DMG
        /// </summary>
        public const int DEFAULT_DMG = 100;
        /// <summary>
        /// Initializes a new instance of the <see cref="Beam" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        /// <param name="immovable">if set to <c>true</c> [immovable].</param>
        public Beam(Vector2 position, Vector2 velocity, int damage, bool immovable = true)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["beam"];
            Damage = damage;
            Immovable = immovable;
            ProjectileType = Type.Beam;
        }

        /// <summary>
        /// Draws from enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            Draw(ref spriteBatch, Color.Red);
        }

        /* @Pre: beam projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the beam does
         * @Return: None
         */
        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
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
    }
}