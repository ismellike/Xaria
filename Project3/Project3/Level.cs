using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using Xaria.Enemies;

namespace Xaria
{
    /// <summary>
    /// The Level class
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Gets the difficulty.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        public int Difficulty { get; private set; }
        /// <summary>
        /// The enemies
        /// </summary>
        public List<List<Enemy>> Enemies = new List<List<Enemy>>();
        /// <summary>
        /// The spacing between enemies
        /// </summary>
        private readonly Vector2 spacing = new Vector2(50, 30);
        /// <summary>
        /// The projectiles of enemies
        /// </summary>
        public List<Projectile> Projectiles = new List<Projectile>();
        /// <summary>
        /// Bool for enemy movements to right or left
        /// </summary>
        private bool movingRight = true;
        /// <summary>
        /// The enemies per row
        /// </summary>
        private const int ENEMIES_PER_ROW = 7;
        /// <summary>
        /// A random class for determining enemy shooting
        /// </summary>
        public static Random random = new Random();

        /// <summary>
        /// Initializes a new instance of the <see cref="Level" /> class.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        public Level(int difficulty)
        {
            Difficulty = difficulty;
            GenerateLevel(Difficulty);
        }

        /// <summary>
        /// Generates the level.
        /// </summary>
        /// <param name="difficulty">The difficulty.</param>
        private void GenerateLevel(int difficulty)
        {
            Enemies.Clear();
            for (int i = 1; i <= difficulty; i++) //use rows for difficulty
            {
                Enemies.Add(new List<Enemy>());
                for (int x = 1; x <= ENEMIES_PER_ROW; x++) //10 enemies per row
                    Enemies[i - 1].Add(new Basic(new Vector2((Game1.textureDictionary["basic"].Width + spacing.X) * x - spacing.X + Game1.textureDictionary["basic"].Width * (i % 2), (Game1.textureDictionary["basic"].Height + spacing.Y) * i + spacing.Y)));
            }
        }

        /// <summary>
        /// Updates the Level's player, enemy projectiles, and enemies.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="touchCollection">The touch collection.</param>
        internal void Update(GameTime gameTime,TouchLocation[] touches)
        {
            #region Update Player
            Game1.player.Update(touches, ref Enemies);
            Game1.player.Shoot(gameTime);
            #endregion
            #region Update Enemies
            if (Enemies.Count == 0)
                NextLevel();
            for (int rowIndex = Enemies.Count - 1; rowIndex >= 0; rowIndex--) //move right to left then move down
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
            for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
            {
                Projectiles[projectileIndex].Position += Projectiles[projectileIndex].Velocity;
                if (Game1.player.IsHit(Projectiles[projectileIndex]))
                {
                    Game1.player.Health -= Projectiles[projectileIndex].Damage;
                    if (Game1.player.Health <= 0)
                        GameOver();
                    Projectiles.RemoveAt(projectileIndex);
                }
            }
            #endregion
        }

        /// <summary>
        /// Goes to the next level.
        /// </summary>
        private void NextLevel()
        {
            Difficulty++;
            GenerateLevel(Difficulty);
        }

        /// <summary>
        /// Draws the Level's enemies and enemy projectiles.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        internal void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    enemy.Draw(ref spriteBatch);
                }
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw(ref spriteBatch, Color.Red);
            }
        }

        /// <summary>
        /// Moves the enemies down.
        /// </summary>
        private void MoveDown()
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    enemy.Position.Y += (enemy.Texture.Height + spacing.Y);
                    if (enemy.Position.Y >= Game1.screenSize.Y - Game1.textureDictionary["ship"].Height - 5)
                    {
                        GameOver();
                    }
                }
        }

        /// <summary>
        /// Ends the game
        /// </summary>
        private void GameOver()
        {
            Game1.state = GameState.End;
        }
    }
}