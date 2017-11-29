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
    /// <seealso cref="Xaria.GameElement" />
    public abstract class Enemy : GameElement
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The basic
            /// </summary>
            Basic = 1,
            /// <summary>
            /// The intermediate
            /// </summary>
            Intermediate = 2,
            /// <summary>
            /// The advanced
            /// </summary>
            Advanced = 3,
            /// <summary>
            /// The boss
            /// </summary>
            Boss = 4,
        }

        /// <summary>
        /// Gets or sets the type of the enemy.
        /// </summary>
        /// <value>
        /// The type of the enemy.
        /// </value>
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
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <value>
        /// The next shoot.
        /// </value>

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString(), Position + new Vector2(10, -25), Color.Red);
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        /// <summary>
        /// Shoots the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Projectiles">The projectiles.</param>
        public abstract void Shoot(GameTime gameTime, ref List<Projectile> Projectiles); //can be inherited by enemies to shoot projectiles down

        internal int GetHealth()
        {
            return Health;
        }

        //returns true if hit so the projectile can be deleted
        /// <summary>
        /// Determines whether the specified shot is hit.
        /// </summary>
        /// <param name="shot">The shot.</param>
        /// <returns>
        ///   <c>true</c> if the specified shot is hit; otherwise, <c>false</c>.
        /// </returns>
        public bool IsHit(Projectile shot)
        {
            if (Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }

        /// <summary>
        /// Updates the movement.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="gameTime">The game time.</param>
        public abstract void UpdateMovement(Level level, GameTime gameTime);

        /// <summary>
        /// Called when [death].
        /// </summary>
        /// <param name="drops">The drops.</param>
        internal abstract void OnDeath(ref List<Drop> drops);

        /// <summary>
        /// Damages the specified damage.
        /// </summary>
        /// <param name="damage">The damage.</param>
        public void Damage(int damage)
        {
            Health -= damage;
        }

        /// <summary>
        /// Determines whether this instance is dead.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is dead; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDead()
        {
            return Health <= 0;
        }

        /// <summary>
        /// Determines whether this instance is boss.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is boss; otherwise, <c>false</c>.
        /// </returns>
        public bool IsBoss()
        {
            return EnemyType == Type.Boss;
        }
    }
}