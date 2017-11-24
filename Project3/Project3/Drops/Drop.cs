using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Drops
{
    public abstract class Drop : GameElement
    {
        //make drop textures around 32x32
        public const float DROP_SPEED = 10f;

        public readonly Vector2 Velocity = new Vector2(0, DROP_SPEED);

        public abstract void OnReceive(ref Player player);

        public override abstract string ToString();

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
            spriteBatch.DrawString(Game1.font, ToString(), new Vector2(Position.X, Position.Y + Texture.Height + 2f), Color.White);
        }
    }
}