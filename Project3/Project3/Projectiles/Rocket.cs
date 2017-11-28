﻿using System.Collections.Generic;
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
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }

        public Rectangle AOE()
        {
            return new Rectangle((int)Position.X - 100, (int)Position.Y - 100, 200, 200);
        }

        public override void Move()
        {
            Position += Velocity;
            Velocity = new Vector2(Velocity.X, Velocity.Y * 1.01f);
        }

        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.FlipVertically, 0f);
        }
    }
}