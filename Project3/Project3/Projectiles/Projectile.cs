using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Xaria
{
    /// <summary>
    /// The Projectile class
    /// </summary>
    /// <seealso cref="Xaria.GameElement" />
    public abstract class Projectile : GameElement//can be an inherited class for different types of projectiles
    {
        /// <summary>
        /// 
        /// </summary>
        public enum Type
        {
            /// <summary>
            /// The laser
            /// </summary>
            Laser,
            /// <summary>
            /// The rocket
            /// </summary>
            Rocket,
            /// <summary>
            /// The beam
            /// </summary>
            Beam,
            /// <summary>
            /// The emp
            /// </summary>
            Emp,
            /// <summary>
            /// The pellet
            /// </summary>
            Pellet,
        }

        /// <summary>
        /// Gets or sets the type of the projectile.
        /// </summary>
        /// <value>
        /// The type of the projectile.
        /// </value>
        protected Type ProjectileType {get; set;}
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

        /// <summary>
        /// The immovable
        /// </summary>
        protected bool Immovable = false;

        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="Enemies">The enemies.</param>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        public abstract void OnCollision(ref List<List<Enemy>> Enemies, int y, int x);

        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="player">The player.</param>
        public abstract void OnCollision(ref Player player);

        /// <summary>
        /// @Pre: RNG is a success for creating an enemy projectile
        /// @Post: A projectile is created below the enemy
        /// @Return: None
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public abstract void DrawFromEnemy(ref SpriteBatch spriteBatch);

        /// <summary>
        /// @Pre: A level is being played. The specified projectile has been fired.
        /// @Post: Projectile moves downwards at the rate of its velocity
        /// @Return: None
        /// </summary>
        public virtual void Move()
        {
            Position += Velocity;
        }

        /// <summary>
        /// @Pre: Projectile is not needed to be removed (mainly beam)
        /// @Post: None.
        /// @Return: True if the projectile is not to be removed when intersecting an enemy or player, false otherwise
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is immovable; otherwise, <c>false</c>.
        /// </returns>
        public bool IsImmovable()
        {
            return Immovable;
        }
    }
}