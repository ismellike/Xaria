using Microsoft.Xna.Framework;
using Project3.Enemies;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;

namespace Project3
{
    public class Level
    {
        public int Difficulty { get; private set; }
        public List<Enemy> Enemies = new List<Enemy>();

        public Level(int difficulty)
        {
            Difficulty = difficulty;
            GenerateLevel(Difficulty);
        }

        private void GenerateLevel(int difficulty)
        {
            Enemies.Clear();
            if(difficulty % 5 == 0) //max 5 rows then do something else
            {

            }
            else
            {
                for(int i = 0; i < difficulty; i++) //use rows for difficulty
                {
                    for(int x = 1; x <= 10; x++) //10 enemies per row
                        Enemies.Add(new Basic(100, new Vector2((Game1.textureDictionary["basic"].Width+40)*x - 40, (Game1.textureDictionary["basic"].Height+30)*i + 30)));
                }
            }
        }

        internal void Update()
        {
            //move enemies
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            Enemies.ForEach((Enemy enemy) =>
            {
                spriteBatch.Draw(enemy.Texture, enemy.Position, null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            });
        }
    }
}