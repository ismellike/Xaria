using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3.Enemies
{
    class Basic : Enemy
    {
        public Basic(Vector2 position, uint tier)
        {
            Health = (int)(100 + 50*tier);
            Position = position;
            Texture = Game1.textureDictionary["basic"];
            Tier = getTier(tier);
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, null, Tier, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
        }
    }
}