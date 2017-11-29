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
        /// @Pre: Enemy is needed to be drawn for the current level being generated
        /// @Post: Enemy is drawn
        /// @Return: None.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString(), Position + new Vector2(10, -25), Color.Red);
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        /// <summary>
        /// @Pre: RNG is a success for the enemy to shoot
        /// @Post: A projectile is shot if the enemy's fire rate is not on cooldown
        /// @Return: None
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
        /// @Pre: Projectile has been shot at the enemy. Enemy's hitbox intersects the player's projectile
        /// @Post: None.
        /// @Return: True if the projectile bounds intersect the enemy, false otherwise
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
        /// @Pre: Update is being called on the current level.
        /// @Post: Enemy's move the way they need to go.
        /// @Return: None.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="gameTime">The game time.</param>
        public abstract void UpdateMovement(Level level, GameTime gameTime);

        /// <summary>
        /// @Pre: Enemy health is less than 0
        /// @Post: Enemy may drop a powerup
        /// @Return: None
        /// </summary>
        /// <param name="drops">The drops.</param>
        internal abstract void OnDeath(ref List<Drop> drops);

        /// <summary>
        /// @Pre: Enemy hitbox intersects a player projectile's hitbox
        /// @Post: Damage is done to the enemy
        /// @Return: None
        /// </summary>
        /// <param name="damage">The damage.</param>
        public void Damage(int damage)
        {
            Health -= damage;
        }

        /// <summary>
        /// @Pre: Update is being called. Need to check if the enemy has died
        /// @Post: None
        /// @Return: true if the enemy health is less than or equal to 0, false otherwise
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is dead; otherwise, <c>false</c>.
        /// </returns>
        public bool IsDead()
        {
            return Health <= 0;
        }

        /// <summary>
        /// @Pre: Update is being called. Need to check if the enemy type is a boss
        /// @Post: None.
        /// @Return: True if the enemyType is boss, false otherwise
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