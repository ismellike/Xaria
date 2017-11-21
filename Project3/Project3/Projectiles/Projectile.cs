using Microsoft.Xna.Framework;
using System.Collections.Generic;

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
        public int Damage { get; internal set; }
        /// <summary>
        /// Gets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public Vector2 Velocity { get; internal set; }

        public bool Immovable = false;

        public abstract void OnCollision(ref List<List<Enemy>> Enemies, int y, int x);

        public abstract void OnCollision(ref Player player);
    }
}