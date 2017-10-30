using Microsoft.Xna.Framework;

namespace Project3.Enemies
{
    class Basic : Enemy
    {
        public Basic(int health, Vector2 position)
        {
            Health = health;
            Position = position;
            Texture = Game1.textureDictionary["basic"]; //rename Standard if you want
        }
    }
}