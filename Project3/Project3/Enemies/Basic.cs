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
    }
}