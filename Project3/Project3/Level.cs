using Microsoft.Xna.Framework;
using Project3.Enemies;
using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Project3
{
    public class Level
    {
        public int Difficulty { get; private set; }
        public List<List<Enemy>> Enemies = new List<List<Enemy>>();
        private readonly Vector2 spacing = new Vector2(50, 30);
        public List<Projectile> Projectiles = new List<Projectile>();
        private bool movingRight = true;
        private const int ENEMIES_PER_ROW = 7;
        public static Random random = new Random();

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
                        Enemies[i-1].Add(new Basic(new Vector2((Game1.textureDictionary["basic"].Width + spacing.X) * x - spacing.X+ Game1.textureDictionary["basic"].Width * (i%2), (Game1.textureDictionary["basic"].Height+spacing.Y)*i + spacing.Y), (uint)difficulty));
                }
            }
        }

        internal void Update(GameTime gameTime, TouchCollection touchCollection, ref Player player)
        {
            #region Update Player
            player.Update(touchCollection, ref Enemies);
            player.Shoot(gameTime);
            #endregion
            #region Update Enemies
            if (Enemies.Count == 0)
                NextLevel();
            for(int rowIndex = Enemies.Count - 1; rowIndex >=0; rowIndex--) //move right to left then move down
            {
                if (Enemies[rowIndex].Count == 0)
                {
                    Enemies.RemoveAt(rowIndex);
                    continue;
                }
                for (int enemyIndex = Enemies[rowIndex].Count - 1; enemyIndex >= 0; enemyIndex--)
                {
                    Enemy enemy = Enemies[rowIndex][enemyIndex];
                    if (enemy.Health <= 0)
                    {
                        Enemies[rowIndex].RemoveAt(enemyIndex);
                        continue;
                    }

                    enemy.Shoot(gameTime, ref Projectiles);
                    if (movingRight)
                    {
                        enemy.Position.X += 1;
                        if (enemy.Position.X + enemy.Texture.Width >= Game1.screenSize.X)
                        {
                            movingRight = !movingRight;
                            MoveDown();
                        }
                    }
                    else
                    {
                        enemy.Position.X -= 1;
                        if (enemy.Position.X <= 0)
                        {
                            movingRight = !movingRight;
                            MoveDown();
                        }
                    }
                }
            }
            #endregion
            #region Update Projectiles
            for(int projectileIndex = Projectiles.Count- 1; projectileIndex>= 0; projectileIndex--)
            {
                Projectiles[projectileIndex].Position += Projectiles[projectileIndex].Velocity;
                if (player.isPlayerHit(Projectiles[projectileIndex]))
                {
                    player.Health -= Projectiles[projectileIndex].Damage;
                    if (player.Health <= 0)
                        GameOver();
                    Projectiles.RemoveAt(projectileIndex);
                }
            }
            #endregion
        }

        private void NextLevel()
        {
            Difficulty++;
            GenerateLevel(Difficulty);
        }

        internal void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    enemy.Draw(ref spriteBatch);
                }
            foreach(Projectile projectile in Projectiles)
            {
                projectile.Draw(ref spriteBatch, Color.Red);
            }
        }

        private void isHit()
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    /*foreach(Projectile projectile in playerProjectiles)
                    {
                        if(projectile.hitBox.Bounds.Intersects())
                    }*/
                }
        }

        private void MoveDown()
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    enemy.Position.Y += (enemy.Texture.Height + spacing.Y);
                    if(enemy.Position.Y >= Game1.screenSize.Y - Game1.textureDictionary["ship"].Height - 10)
                    {
                        GameOver();
                    }
                }
        }

        private void GameOver()
        {
            Game1.state = GameState.End;
        }
    }
}