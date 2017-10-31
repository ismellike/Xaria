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
        public List<List<Enemy>> Enemies = new List<List<Enemy>>();
        private readonly Vector2 spacing = new Vector2(50, 30);
        private bool movingRight = true;
        private const int ENEMIES_PER_ROW = 7;

        public Level(int difficulty)
        {
            Difficulty = difficulty;
            GenerateLevel(Difficulty);
        }

        private void GenerateLevel(int difficulty)
        {
            Enemies.Clear();
            if(difficulty % 5 == 0) //max 4 rows then do something else
            {

            }
            else
            {
                for (int i =1; i <= difficulty; i++) //use rows for difficulty
                {
                    Enemies.Add(new List<Enemy>());
                    for (int x = 1; x <= ENEMIES_PER_ROW; x++) //10 enemies per row
                        Enemies[i-1].Add(new Basic(new Vector2((Game1.textureDictionary["basic"].Width + spacing.X) * x - spacing.X+ Game1.textureDictionary["basic"].Width * (i%2), (Game1.textureDictionary["basic"].Height+spacing.Y)*i + spacing.Y), (uint)difficulty % 10));
                }
            }
        }

        internal void Update()
        {
            for(int y = Enemies.Count - 1; y >=0; y--) //move right to left then move down
            {
                for (int x = Enemies[y].Count - 1; x >= 0; x--)
                {
                    Enemy enemy = Enemies[y][x];
                    if (enemy.Health <= 0)
                    {
                        Enemies[y].RemoveAt(x);
                        continue;
                    }
                    if (movingRight)
                    {
                        enemy.Position.X += 1;
                        if (enemy.Position.X + enemy.Texture.Width >= Game1.screenSize.X)
                        {
                            movingRight = !movingRight;
                           // enemy.Position.Y += (enemy.Texture.Height + spacing.Y); move all enemies down
                        }
                    }
                    else
                    {
                        enemy.Position.X -= 1;
                        if (enemy.Position.X <= 0)
                        {
                            movingRight = !movingRight;
                            //enemy.Position.Y += (enemy.Texture.Height + spacing.Y); move all enemies down
                        }
                    }
                }
            }
            //move enemies
        }

        internal void Draw(SpriteBatch spriteBatch)
        {
            Enemies.ForEach((List<Enemy> row) =>
            {
                row.ForEach((Enemy enemy) =>
                {
                    spriteBatch.Draw(enemy.Texture, enemy.Position, null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
                });
            });
        }
    }
}