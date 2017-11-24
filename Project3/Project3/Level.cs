using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Enemies;
using Xaria.Projectiles;

namespace Xaria
{
    /// <summary>
    /// The Level class
    /// </summary>
    public class Level
    {
        private Player player;
        /// <summary>
        /// Gets the difficulty.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        public static int Difficulty { get; private set; }
        /// <summary>
        /// The enemies
        /// </summary>
        private List<List<Enemy>> Enemies = new List<List<Enemy>>();
        /// <summary>
        /// The spacing between enemies
        /// </summary>
        private readonly Vector2 spacing = new Vector2(50, 30);
        /// <summary>
        /// The projectiles of enemies
        /// </summary>
        internal List<Projectile> Projectiles = new List<Projectile>();

        internal List<Drop> Drops = new List<Drop>();
        /// <summary>
        /// Bool for enemy movements to right or left
        /// </summary>
        internal bool movingRight = true;
        /// <summary>
        /// The enemies per row
        /// </summary>
        public const int ENEMIES_PER_ROW = 7;
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
            player = new Player();
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
            if (difficulty % 4 == 0)
            {
                Enemies.Add(new List<Enemy>());
                if(difficulty/4 == 1)
                {
                    Enemies[0].Add(new Boss1(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss1"].Width)), spacing.Y)));
                    AddRowOfEnemy(typeof(Basic));
                    AddRowOfEnemy(typeof(Basic));
                }
                else if(difficulty/4 == 2)
                {
                    Enemies[0].Add(new Boss1(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss1"].Width)), spacing.Y)));
                    Enemies[0].Add(new Boss1(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss1"].Width)), spacing.Y)));
                    AddRowOfEnemy(typeof(Basic));
                    AddRowOfEnemy(typeof(Basic));
                }
                else if(difficulty/4 == 3)
                {
                    Enemies[0].Add(new Boss3(new Vector2(Game1.textureDictionary["boss3"].Width, Game1.textureDictionary["boss3"].Height)));
                }
                else if(difficulty/4 == 4)
                {
                    Enemies[0].Add(new Boss4(new Vector2(Game1.textureDictionary["boss4"].Width, Game1.textureDictionary["boss4"].Height)));
                }
            }
            else
            {
                for(int i = 0; i < difficulty % 4; i++)
                {
                    AddRowOfEnemy(typeof(Basic));
                }
            }
        }


        private void AddRowOfEnemy(Type enemyType)
        {
            Enemy prevEnemy = Enemies.Count > 0 ? Enemies[Enemies.Count - 1][0] : null;
            float newPosY = prevEnemy == null ? spacing.Y : prevEnemy.Position.Y + prevEnemy.Texture.Height + spacing.Y;
            Enemies.Add(new List<Enemy>());
            for (int x = 1; x <= ENEMIES_PER_ROW; x++)
            {
                Enemy enemy = null;
                if (enemyType == typeof(Basic))
                {
                    enemy = new Basic(new Vector2((Game1.textureDictionary["basic"].Width + spacing.X) * x - spacing.X + Game1.textureDictionary["basic"].Width * (Enemies.Count % 2), newPosY));
                }
                else if (enemyType == typeof(Intermediate))
                {
                    enemy = new Intermediate(new Vector2((Game1.textureDictionary["intermediate"].Width + spacing.X) * x - spacing.X + Game1.textureDictionary["intermediate"].Width * (Enemies.Count % 2), newPosY));
                }

                Enemies[Enemies.Count - 1].Add(enemy);
            }
        }

        /* @Pre: Player has started the game.
         * @Post: The current level the player is playing is updated. This includes: Player movement, projectiles, drops. Enemy movement and projectiles as well.
         * @Return: None
         */
        internal void Update(GameTime gameTime, TouchLocation[] touches, float roll)
        {
            player.Update(gameTime, touches, roll, ref Enemies);
                UpdateEnemies(gameTime);
                UpdateEnemyProjectiles();
                UpdateDrops();
            if (player.Health <= 0)
                GameOver();
        }

        private void UpdateDrops()
        {
            for (int dropIndex = Drops.Count - 1; dropIndex >= 0; dropIndex--)
            {
                Drops[dropIndex].Position += Drops[dropIndex].Velocity;
                if (player.Intersects(Drops[dropIndex]))
                {
                    Drops[dropIndex].OnReceive(ref player);
                    Drops.RemoveAt(dropIndex);
                }
            }
        }

        private void UpdateEnemies(GameTime gameTime)
        {
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
                    if (enemy.IsDead())
                    {
                        Enemies[rowIndex][enemyIndex].OnDeath(ref Drops);
                        Enemies[rowIndex].RemoveAt(enemyIndex);
                        continue;
                    }
                    enemy.UpdateMovement(this, gameTime);
                    enemy.Shoot(gameTime, ref Projectiles);

                }
            }
        }

        private void UpdateEnemyProjectiles()
        {
            for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
            {
                Projectiles[projectileIndex].Move();
                if (player.Intersects(Projectiles[projectileIndex]))
                {
                    Projectiles[projectileIndex].OnCollision(ref player);
                    Projectiles.RemoveAt(projectileIndex);
                }
            }
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
            player.Draw(ref spriteBatch);
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    enemy.Draw(ref spriteBatch);
                }
            foreach (Projectile projectile in Projectiles)
            {
                projectile.DrawFromEnemy(ref spriteBatch);
            }
            foreach(Drop drop in Drops)
            {
                drop.Draw(ref spriteBatch);
            }
            spriteBatch.DrawString(Game1.font, "Level: " + Difficulty.ToString(), new Vector2(30, 10), Color.White);
        }

        /// <summary>
        /// Moves the enemies down.
        /// </summary>
        internal void MoveDown()
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    if (enemy is Basic || enemy is Intermediate)
                    {
                        enemy.Position.Y += (enemy.Texture.Height + spacing.Y);
                        if (enemy.Position.Y >= Game1.screenSize.Y - Game1.textureDictionary["ship"].Height - 5)
                        {
                            GameOver();
                        }
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