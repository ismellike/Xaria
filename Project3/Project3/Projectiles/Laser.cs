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

        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            Draw(ref spriteBatch, Color.Red);
        }

        /* @Pre: laser projectile overlaps enemy sprite
         * @Post: Enemy's health is reduced by lasers damage
         * @Return: None
         */
        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            Enemies[y][x].Damage(Damage);
        }

        /* @Pre: laser projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the pellet does
         * @Return: None
         */
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }
    }
}