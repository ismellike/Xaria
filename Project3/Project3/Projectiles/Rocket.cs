using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The rocket class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Rocket : Projectile
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
        public Rocket(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["rocket"];
            Damage = damage;
            ProjectileType = Type.Rocket;
        }

        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="Enemies">The enemies.</param>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            Rectangle aoe = AOE();
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    if (enemy.Bounds().Intersects(aoe))
                    {
                        enemy.Damage(Damage);
                    }
                }
            //create explosion effect
        }

        /* @Pre: rocket projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the rocket does
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
        /// Aoes this instance.
        /// </summary>
        /// <returns></returns>
        public Rectangle AOE()
        {
            return new Rectangle((int)Position.X - 100, (int)Position.Y - 100, 200, 200);
        }

        /// <summary>
        /// Moves this instance.
        /// </summary>
        public override void Move()
        {
            Position += Velocity;
            Velocity = new Vector2(Velocity.X, Velocity.Y * 1.01f);
        }

        /// <summary>
        /// Draws from enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
        }
    }
}