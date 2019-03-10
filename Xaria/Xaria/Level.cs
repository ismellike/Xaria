using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Enemies;

namespace Xaria
{
    /// <summary>
    /// The Level class
    /// </summary>
    public class Level
    {
        /// <summary>
        /// The player
        /// </summary>
        internal Player player;
        /// <summary>
        /// Gets the difficulty.
        /// </summary>
        /// <value>
        /// The difficulty.
        /// </value>
        public int Difficulty { get; private set; }

        /// <summary>
        /// The boss level
        /// </summary>
        public const int BOSS_LEVEL = 4;
        /// <summary>
        /// The final level
        /// </summary>
        public const int FINAL_LEVEL = 16;
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

        /// <summary>
        /// The drops
        /// </summary>
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
        /// @Pre: Either the game has just started, or a level was just beaten. Difficulty level is needed.
        /// @Post: The current level the player needs to go through is generated
        /// @Return: None.
        /// </summary>
        /// <param name="difficulty">The difficulty of the level.</param>
        private void GenerateLevel(int difficulty)
        {
            Enemies.Clear();
            //ends the game
            if (difficulty > FINAL_LEVEL)
            {
                GameOver();
                return;
            }
            //adds a boss every 4 levels
            if (difficulty % BOSS_LEVEL == 0)
            {
                switch (difficulty / BOSS_LEVEL)
                {
                    case 1:
                        AddBoss(Boss.Type.Boss1);
                        break;
                    case 2:
                        AddBoss(Boss.Type.Boss2);
                        break;
                    case 3:
                        AddBoss(Boss.Type.Boss3);
                        break;
                    case 4:
                        AddBoss(Boss.Type.Boss4);
                        break;
                }
                for (int i = 0; i < 5; i++)
                    AddRowOfEnemy(Enemy.Type.Basic);
            }
            else
            {
                List<Enemy.Type> types = GetEnemyTypes();
                for(int i = 0; i < types.Count; i++)
                {
                    AddRowOfEnemy(types[i]);
                }
            }
        }

        /// <summary>
        /// @Pre: Requires level difficulty. A new level is needed to be generated.
        /// @Post: The previous level's row types for the enemy rows is found
        /// @Return: enemy list for the current level
        /// </summary>
        /// <param name="prevLevel">The previous level.</param>
        /// <param name="difficulty">The difficulty.</param>
        /// <returns></returns>
        private List<Enemy.Type> GetEnemyTypes(List<Enemy.Type> prevLevel = null, int difficulty = 1)
        {
            if (difficulty % BOSS_LEVEL == 0)
                return GetEnemyTypes(prevLevel, difficulty + 1);

            if (difficulty == 1)
                prevLevel = new List<Enemy.Type>() { Enemy.Type.Basic, Enemy.Type.Basic};
            else if (difficulty % 4 == 1 && prevLevel.Count < 5)
                prevLevel.Add(Enemy.Type.Basic);
            else
            {
                if (prevLevel.Contains(Enemy.Type.Basic))
                    prevLevel[prevLevel.IndexOf(Enemy.Type.Basic)] = Enemy.Type.Intermediate;
                else if (prevLevel.Contains(Enemy.Type.Intermediate))
                    prevLevel[prevLevel.IndexOf(Enemy.Type.Intermediate)] = Enemy.Type.Advanced;
            }

            if (Difficulty <= difficulty)
                return prevLevel;
            else
                return GetEnemyTypes(prevLevel, difficulty + 1);
        }
        /// <summary>
        /// @Pre: Level 3, 7, 11, or 15 has been completed. A boss is needed for the next level and is generated.
        /// @Post: Boss is generated
        /// @Return: None
        /// </summary>
        /// <param name="bossType">Type of the boss.</param>
        private void AddBoss(Boss.Type bossType)
        {
            Enemy prevEnemy = Enemies.Count > 0 ? Enemies[Enemies.Count - 1][0] : null;
            float newPosY = prevEnemy == null ? spacing.Y : prevEnemy.Position.Y + prevEnemy.Texture.Height + spacing.Y;

            Enemies.Add(new List<Enemy>());
            switch (bossType)
            {
                case Boss.Type.Boss1:
                    Enemies[Enemies.Count - 1].Add(new Boss1(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss1"].Width)), newPosY)));
                    break;
                case Boss.Type.Boss2:
                    Enemies[Enemies.Count - 1].Add(new Boss2(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss2"].Width)), newPosY)));
                    break;
                case Boss.Type.Boss3:
                    Enemies[Enemies.Count - 1].Add(new Boss3(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss3"].Width)), newPosY)));
                    break;
                case Boss.Type.Boss4:
                    Enemies[Enemies.Count - 1].Add(new Boss4(new Vector2(random.Next(((int)Game1.screenSize.X - Game1.textureDictionary["boss4"].Width)), newPosY)));
                    break;
            }
        }

        /// <summary>
        /// @Pre: A row of enemies is needed for the next level
        /// @Post: A level of enemies (basic, intermediate, or advanced) is added to the enemy list
        /// @Return: None.
        /// </summary>
        /// <param name="enemyType">Type of the enemy.</param>
        private void AddRowOfEnemy(Enemy.Type enemyType)
        {
            Enemy prevEnemy = Enemies.Count > 0 ? Enemies[Enemies.Count - 1][0] : null;
            float newPosY = prevEnemy == null ? spacing.Y : prevEnemy.Position.Y + prevEnemy.Texture.Height + spacing.Y;
            Enemies.Add(new List<Enemy>());
            for (int x = 1; x <= ENEMIES_PER_ROW; x++)
            {
                Enemy enemy = null;

                switch (enemyType)
                {
                    case Enemy.Type.Basic:
                        enemy = new Basic(new Vector2(((Game1.textureDictionary["basic"].Width + spacing.X) * x) - spacing.X + (Game1.textureDictionary["basic"].Width * (Enemies.Count % 2)), newPosY));
                        break;
                    case Enemy.Type.Intermediate:
                        enemy = new Intermediate(new Vector2(((Game1.textureDictionary["intermediate"].Width + spacing.X) * x) - spacing.X + (Game1.textureDictionary["intermediate"].Width * (Enemies.Count % 2)), newPosY));
                        break;
                    case Enemy.Type.Advanced:
                        enemy = new Advanced(new Vector2(((Game1.textureDictionary["advanced"].Width + spacing.X) * x) - spacing.X + (Game1.textureDictionary["advanced"].Width * (Enemies.Count % 2)), newPosY));
                        break;
                }

                Enemies[Enemies.Count - 1].Add(enemy);
            }
        }

        /// <summary>
        ///@Pre: Player has started the game.
        ///@Post: The current level the player is playing is updated.This includes: Player movement, projectiles, drops. Enemy movement and projectiles as well.
        ///@Return: None
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="touches">The touches.</param>
        /// <param name="roll">The roll.</param>
        internal void Update(GameTime gameTime, TouchLocation[] touches, float roll)
        {
            player.Update(gameTime, touches, roll, ref Enemies);
                UpdateEnemies(gameTime);
                UpdateEnemyProjectiles();
                UpdateDrops();
            if (player.Health <= 0)
                GameOver();
        }
        /// <summary>
        /// @Pre: An enemy has died and powerup drop for the player has been created
        /// @Post: powerup drop is moved downward until it is off the screen or the player's hitbox intersects the drops hitbox
        /// @Return: None
        /// </summary>
        private void UpdateDrops()
        {
            //checks every drop, then checks if the drop intersects the player. If so, the player gains the powerup
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

        /// <summary>
        /// @Pre: The list of enemies has been created.
        /// @Post: All enemy sprites are updated. This means if their health is below 0, they are removed from the list and the game. Calls UpdateMovement and Shoot on all enemies.
        /// @Return: None
        /// </summary>
        /// <param name="gameTime">The game time.</param>
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
                //updates every enemies movement and checks if their health is below 0
                for (int enemyIndex = Enemies[rowIndex].Count - 1; enemyIndex >= 0; enemyIndex--)
                {
                    Enemy enemy = Enemies[rowIndex][enemyIndex];
                    if (enemy.IsDead())
                    {
                        Enemies[rowIndex][enemyIndex].OnDeath(ref Drops); //checks if a drop is needed
                        Enemies[rowIndex].RemoveAt(enemyIndex); //removes enemy from the list
                        continue;
                    }
                    enemy.UpdateMovement(this, gameTime);
                    enemy.Shoot(gameTime, ref Projectiles);
                }
            }
        }

        /// <summary>
        /// @Pre: An enemy has shot a projectile (decided by RNG)
        /// @Post: All enemy projectiles are moved downward.If it intersects the player, the player loses health.
        /// @Return: None
        /// </summary>
        private void UpdateEnemyProjectiles()
        {
            //checks every projectile then updates their movement
            for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
            {
                Projectiles[projectileIndex].Move();
                //if the projectile intersects the player it is removed and the player is damaged
                if (player.Intersects(Projectiles[projectileIndex]))
                {
                    Projectiles[projectileIndex].OnCollision(ref player);
                    Projectiles.RemoveAt(projectileIndex);
                }
            }
        }

        /// <summary>
        /// Goes to the next level.
        /// @Pre: An enemy has shot a projectile (decided by RNG)
        /// @Post: All enemy projectiles are moved downward.If it intersects the player, the player loses health.
        /// @Return: None
        /// </summary>
        private void NextLevel()
        {
            Difficulty++;
            GenerateLevel(Difficulty);
        }

        /// <summary>
        /// @Pre: The game has been started or the previous level has ended. The sprites are needed to be drawn.
        /// @Post: Enemy sprites are drawn to screen
        /// @Return: None
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        internal void Draw(ref SpriteBatch spriteBatch)
        {
            //draws each enemy and projectile
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
        /// @Pre: Enemy sprite's texture has reached the edge of the screen from either moving right or left. If they were moving right, this would mean the sprite(s) all the way on the right has reached the edge of the right side of the screen, and vice versa.
        /// @Post: All enemies move downward a row.
        /// @Return: None
        /// </summary>
        internal void MoveDown()
        {
            foreach (List<Enemy> row in Enemies)
                foreach (Enemy enemy in row)
                {
                    if (!enemy.IsBoss())
                    {
                        enemy.Position.Y += (3*spacing.Y);
                        //if the ships get right above the player, the player loses the game
                        if (enemy.Position.Y >= Game1.screenSize.Y - Game1.textureDictionary["ship"].Height - 5)
                        {
                            GameOver();
                        }
                    }
                }
        }

        /// <summary>
        /// @Pre: Player has beaten the final level (level 16) or has lost all of their lives
        /// @Post: GameState is changed to End
        /// @Return: None
        /// </summary>
        private void GameOver()
        {
            Game1.state = GameState.End;
        }
    }
}