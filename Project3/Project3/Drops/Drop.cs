using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Drops
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xaria.GameElement" />
    public abstract class Drop : GameElement
    {
        //make drop textures around 32x32
        /// <summary>
        /// The drop speed
        /// </summary>
        public const float DROP_SPEED = 10f;

        /// <summary>
        /// The velocity
        /// </summary>
        public readonly Vector2 Velocity = new Vector2(0, DROP_SPEED);

        /// <summary>
        /// Called when [receive].
        /// </summary>
        /// <param name="player">The player.</param>
        public abstract void OnReceive(ref Player player);

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public override abstract string ToString();

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.DrawString(Game1.font, ToString(), new Vector2(Position.X, Position.Y + Texture.Height + 2f), Color.White);
        }
    }
}