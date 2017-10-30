namespace Project3.Enemies
{
    class Basic : Enemy
    {
        public Basic(int health)
        {
            Health = health;
            Texture = Game1.textureDictionary["basic"]; //rename Standard if you want
        }
    }
}