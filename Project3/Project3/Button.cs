using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    class Button
    {
        public Texture2D Texture { get; internal set; }
        public Vector2 Position { get; internal set; }

        public Button(Texture2D texture, Vector2 position)
        {
            Texture = texture;
            Position = position;
        }

        public bool isClicked(Vector2 input)
        {
            if (Texture.Width + Position.X < input.X && Texture.Height - Position.Y > input.X
                && Texture.Height + Position.Y < input.Y && Texture.Height - Position.Y > input.Y)
                return true;
            return false;
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}