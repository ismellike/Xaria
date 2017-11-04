using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria
{
    /// <summary>
    /// The Projectile class
    /// </summary>
    public class Projectile : GameElement//can be an inherited class for different types of projectiles
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
    }
}