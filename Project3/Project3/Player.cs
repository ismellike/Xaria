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
    class Player : GameElement
    {

        public int Health { get; internal set; }
        public List<Projectile> Projectiles = new List<Projectile>();
        public double ShootCooldown { get; internal set; }
        internal double nextShoot; //start at ShootCooldown go to 0 then reset
        public float Velocity = 10;
        internal const int STARTING_HEALTH = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        public Player()
        {
            Health = STARTING_HEALTH;
            Texture = Game1.textureDictionary["ship"];
            Position = new Vector2((Game1.screenSize.X + Texture.Width)/ 2f, Game1.screenSize.Y - Texture.Height - 10);
            ShootCooldown = .5; //seconds between shots
            nextShoot = ShootCooldown;
        }

        /// <summary>
        /// Updates the specified touch and checks for player projectile collision with enemies.
        /// </summary>
        /// <param name="touch">The touch.</param>
        /// <param name="Enemies">The enemies.</param>
        internal void Update(TouchCollection touch, ref List<List<Enemy>> Enemies)
        {
            //move the player
            if (touch.Count > 0)
                if (Math.Abs(touch[0].Position.X - Position.X - Texture.Width / 2f) > Velocity * 1.25f) //stops oscillation
                {
                    if (touch[0].Position.X < Position.X + Texture.Width / 2f)
                    {
                        Position.X -= Velocity;
                    }
                    else
                    {
                        Position.X += Velocity;
                    }
                }
            //move their projectiles
            for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
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
                            Enemies[y][x].Health -= projectile.Damage;
                            Projectiles.RemoveAt(projectileIndex);
                            goto exit;
                        }
                    }
                exit:;
            }
        }

        /// <summary>
        /// Draws the player, projectiles, and health.
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString(), Position + new Vector2(10, -25), Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(Texture, Position, null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            foreach (Projectile projectile in Projectiles)
            {
                projectile.Draw(ref spriteBatch);
            }
        }

        /// <summary>
        /// Shoots a projectile after a given time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        internal void Shoot(GameTime gameTime)
        {
            nextShoot -= gameTime.ElapsedGameTime.TotalSeconds;
            if (nextShoot <= 0)
            {
                nextShoot = ShootCooldown;
                Projectiles.Add(new Laser(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -30), 50)); //moving up
            }
        }

        /// <summary>
        /// Determines whether the player is hit by a projectile.
        /// </summary>
        /// <param name="shot">The projectile.</param>
        /// <returns>
        ///   <c>true</c> if the specified player is hit; otherwise, <c>false</c>.
        /// </returns>
        public bool IsHit(Projectile shot)
        {
            if (Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }
    }
}