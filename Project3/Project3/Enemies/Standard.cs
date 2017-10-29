namespace Project3.Enemies
{
    class Standard : Enemy
    {
        public Standard(int health)
        {
            Health = health;
            Texture = Game1.textureDictionary["Standard"]; //rename Standard if you want
        }
    }
}