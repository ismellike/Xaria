using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Enemies;

namespace Xaria
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    public abstract class Enemy : GameElement
    {
        public enum Type
        {
            Basic = 1,
            Intermediate = 2,
            Advanced = 3,
            Boss = 4,
        }

        protected Type EnemyType { get; set; }
        /// <summary>
        /// Gets the health.
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
        protected int Health { get; set; }
        /// <summary>
        /// Gets the next shoot.
        /// </summary>
        /// <value>
        /// The next shoot.
        /// </value>
        protected double NextShoot { get; set; }
        /// <summary>
        /// Gets the next shoot.
        /// </summary>
        /// <value>
        /// The next shoot.
        /// </value>

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString(), Position + new Vector2(10, -25), Color.Red);
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        public abstract void Shoot(GameTime gameTime, ref List<Projectile> Projectiles); //can be inherited by enemies to shoot projectiles down

        //returns true if hit so the projectile can be deleted
        public bool IsHit(Projectile shot)
        {
            if (Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }

        public abstract void UpdateMovement(Level level, GameTime gameTime);

        internal abstract void OnDeath(ref List<Drop> drops);

        public void Damage(int damage)
        {
            Health -= damage;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}