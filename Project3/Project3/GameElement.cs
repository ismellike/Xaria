using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria
{
    /// <summary>
    /// Basic element that stores texture, position, and draw info.
    /// </summary>
    public class GameElement
    {
        /// <summary>
        /// Gets the texture.
        /// </summary>
        /// <value>
        /// The texture.
        /// </value>
        public Texture2D Texture { get; internal set; }
        /// <summary>
        /// The position
        /// </summary>
        internal Vector2 Position;

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="color">The color.</param>
        public void Draw(ref SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, Position, color);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public virtual void Draw(ref SpriteBatch spriteBatch)
        {
            Draw(ref spriteBatch, Color.White);
        }

        /// <summary>
        /// Gets the element's bounds
        /// </summary>
        /// <returns></returns>
        public Rectangle Bounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}