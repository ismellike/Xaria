using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    public class GameElement
    {
        public Texture2D Texture { get; internal set; }
        internal Vector2 Position;

        public void Draw(ref SpriteBatch spriteBatch, Color color)
        {
            spriteBatch.Draw(Texture, Position, null, color, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
        }

        public virtual void Draw(ref SpriteBatch spriteBatch)
        {
            Draw(ref spriteBatch, Color.White);
        }

        public Rectangle Bounds()
        {
            return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
        }
    }
}