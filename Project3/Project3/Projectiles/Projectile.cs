using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace Xaria
{
    /// <summary>
    /// The Projectile class
    /// </summary>
    public abstract class Projectile : GameElement//can be an inherited class for different types of projectiles
    {
        /// <summary>
        /// Gets the damage.
        /// </summary>
        /// <value>
        /// The damage.
        /// </value>
        protected int Damage { get; set; }
        /// <summary>
        /// Gets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        protected Vector2 Velocity { get; set; }

        protected bool Immovable = false;

        public abstract void OnCollision(ref List<List<Enemy>> Enemies, int y, int x);

        public abstract void OnCollision(ref Player player);

        public abstract void DrawFromEnemy(ref SpriteBatch spriteBatch);

        public virtual void Move()
        {
            Position += Velocity;
        }

        public bool IsImmovable()
        {
            return Immovable;
        }
    }
}