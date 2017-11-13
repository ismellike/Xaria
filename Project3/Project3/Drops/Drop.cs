using Microsoft.Xna.Framework;

namespace Xaria.Drops
{
    public class Drop : GameElement
    {
        /// <summary>
        /// Gets the velocity.
        /// </summary>
        /// <value>
        /// The velocity.
        /// </value>
        public Vector2 Velocity { get; internal set; }

        public virtual void OnReceive()
        {

        }
    }
}