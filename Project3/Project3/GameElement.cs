using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    class GameElement
    {
        public Texture2D Texture { get; internal set; }
        internal Vector2 Position;

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
        }
    }
}