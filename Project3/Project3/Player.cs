using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using Xaria.Projectiles;
using System;
using System.Collections.Generic;

namespace Xaria
{
    /// <summary>
    /// Our player class
    /// </summary>
    public class Player : GameElement
    {
        public int ExtraLives { get; internal set; }
        public int Health { get; internal set; }
        internal List<Projectile> Projectiles = new List<Projectile>();
        internal const int STARTING_HEALTH = 100;
        public int Shield { get; internal set; }
        public double stunned = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Health = STARTING_HEALTH;
            Texture = Game1.textureDictionary["ship"];
            Position = new Vector2((Game1.screenSize.X + Texture.Width)/ 2f, Game1.screenSize.Y - Texture.Height - 10);
            ExtraLives = 0;
        }

        /// <summary>
        /// Updates the specified touch and checks for player projectile collision with enemies.
        /// </summary>
        /// <param name="touch">The touch.</param>
        /// <param name="Enemies">The enemies.</param>
        internal void Update(TouchCollection touches, float roll, ref List<List<Enemy>> Enemies, GameTime gameTime)
        {
<<<<<<< HEAD
            stunned -= gameTime.ElapsedGameTime.milliseconds;
            if (stunned >= 0)
=======
           if (Math.Abs(roll) > 3) //code for moving player with rotation
>>>>>>> 82cb159b0c63d65d68836bf53f086f87f6a6ee2b
            {
                //player can't do anything.
            }
            else
            {
                if (Math.Abs(roll) > 3)
                {
                    if (Position.X + roll <= 0)
                    {
                        Position.X = 0;
                    }
                    else if (Position.X + Texture.Width + roll >= Game1.screenSize.X)
                    {
                        Position.X = Game1.screenSize.X - Texture.Width;
                    }
                    else
                    {
                        Position.X += roll;
                    }
                }
                if (touches.Count > 0)
                {
                    Shoot();
                }
<<<<<<< HEAD
                //move their projectiles
                for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
=======
            }
           foreach(TouchLocation touch in touches)
            { 
                    if (touch.State == TouchLocationState.Released)
                        Shoot();
            }
            //move their projectiles
            for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
            {
                Projectile projectile = Projectiles[projectileIndex];
                if (projectile.Position.X <= 0)
>>>>>>> 82cb159b0c63d65d68836bf53f086f87f6a6ee2b
                {
                    Projectile projectile = Projectiles[projectileIndex];
                    if (projectile.Position.X <= 0)
                    {
                        Projectiles.RemoveAt(projectileIndex);
                        continue;
                    }
                    projectile.Position += projectile.Velocity;

                    for (int y = Enemies.Count - 1; y >= 0; y--)
                        for (int x = Enemies[y].Count - 1; x >= 0; x--) //lower rows are more likely to be hit by a projecitle
                        {
                            if (Enemies[y][x].IsHit(projectile))
                            {
                                projectile.OnCollision(ref Enemies, y, x);
                                Projectiles.RemoveAt(projectileIndex);
                                goto exit;
                            }
                        }
                    exit:;
                }
            }
        }

        /// <summary>
        /// Draws the player, projectiles, and health.
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString() + (ExtraLives > 0 ? " + " + ExtraLives.ToString(): ""), Position + new Vector2(-5, -25), Color.LimeGreen);
            if(Shield>0)
                spriteBatch.DrawString(Game1.font, Shield.ToString(), Position +  new Vector2(Texture.Width - 25, -25), Color.Cyan);
            spriteBatch.Draw(Texture, Position, Color.White);
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw(ref spriteBatch);
            }
        }

        internal void Reset()
        {
            Projectiles.Clear();
            Health = STARTING_HEALTH;
        }

        /// <summary>
        /// Shoots a projectile after a given time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        internal void Shoot()
        {
                Projectiles.Add(new Laser(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -30), 50)); //moving up
        }

        public bool Intersects(GameElement shot)
        {
            if (Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }

        internal void Damage(int damage)
        {
            if(Shield > 0)
            {
                if (Shield < damage)
                {
                    damage -= Shield;
                    Shield = 0;
                    Health -= damage;
                }
                else
                {
                    Shield -= damage;
                }
            }
            else
                Health -= damage;
            if(Health <= 0)
            {
                if (ExtraLives >= 1)
                {
                    ExtraLives--;
                    Health = 100;
                }
            }
        }
    }
}