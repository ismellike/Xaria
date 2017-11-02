using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using Project3.Projectiles;
using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Our player class
    /// </summary>
    class Player : GameElement
    {
        public int Health { get; internal set; }
        public List<Projectile> playerProjectiles = new List<Projectile>();
        public double ShootCooldown { get; internal set; }
        internal double nextShoot; //start at ShootCooldown go to 0 then reset
        public float Velocity = 10;

        public Player(int health)
        {
            Health = health;
            Texture = Game1.textureDictionary["ship"];
            Position = new Vector2((Game1.screenSize.X + Texture.Width)/ 2f, Game1.screenSize.Y - Texture.Height - 10);
            ShootCooldown = .5; //seconds between shots
            nextShoot = ShootCooldown;
        }

        internal void Update(TouchCollection touch)
        {
            //move the player
            if(touch.Count > 0)
            if (Math.Abs(touch[0].Position.X - Position.X - Texture.Width / 2f) > Velocity*1.25f) //stops oscillation
            {
                if (touch[0].Position.X < Position.X + Texture.Width/2f)
                {
                    Position.X -= Velocity;
                }
                else
                {
                    Position.X += Velocity;
                }
            }
            //move their projectiles
            for (int i = playerProjectiles.Count - 1; i >= 0; i--)
            {
                Projectile projectile = playerProjectiles[i];
                if (projectile.Position.X <= 0)
                {
                    playerProjectiles.Remove(projectile);
                    continue;
                }
                projectile.Position += projectile.Velocity;
            }
        }

        internal void Shoot(GameTime gameTime)
        {
            nextShoot -= gameTime.ElapsedGameTime.TotalSeconds;
            if (nextShoot <= 0)
            {
                nextShoot = ShootCooldown;
                playerProjectiles.Add(new Laser(Position+ new Vector2(Texture.Width/2f, Texture.Height), new Vector2(0, -30))); //moving up
            }
        }

        public bool isPlayerHit(Projectile shot)
        {
            Rectangle hitBox = new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height);
            /*if (hitBox.Intersects(shot.hitBox.Bounds))
            { 
                lowerHealth(shot.Damage);
                return true;
            }*/
            return false;
        }

        private void lowerHealth(int damage)
        {
            Health -= damage;
        }
    }
}