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
        public struct Weapon
        {
            public Type projectileType { get; internal set; }
            public int ammo { get; internal set; }
            public bool infinite { get; internal set; }

            public Weapon(Type ProjectileType, int Ammo, bool Infinite = false)
            {
                projectileType = ProjectileType;
                ammo = Ammo;
                infinite = Infinite;
            }
        }
        internal List<Weapon> Weapons = new List<Weapon>() { new Weapon(typeof(Laser), 1, true), new Weapon(typeof(Rocket), 20) };
        private Weapon weapon = new Weapon(typeof(Laser), 1, true);
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
            Position = new Vector2((Game1.screenSize.X + Texture.Width)/ 2f, Game1.screenSize.Y - Texture.Height - 25);
            ExtraLives = 0;
        }

        /// <summary>
        /// Updates the specified touch and checks for player projectile collision with enemies.
        /// </summary>
        /// <param name="touch">The touch.</param>
        /// <param name="Enemies">The enemies.</param>
        internal void Update(GameTime gameTime, TouchLocation[] touches, float roll, ref List<List<Enemy>> Enemies)
        {
            if (stunned >= 0)
            {
                stunned -= gameTime.ElapsedGameTime.Milliseconds;
            }
            else if (Math.Abs(roll) > 3)
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
            }
            foreach (TouchLocation touch in touches)
            {
                if (touch.State == TouchLocationState.Released)
                {
                    if (IsClicked(touch.Position))
                        SwitchWeapon();
                    else
                        Shoot();
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
                            projectile.OnCollision(ref Enemies, y, x);
                            if(!projectile.Immovable)
                                Projectiles.RemoveAt(projectileIndex);
                            goto exit;
                        }
                    }
                exit:;
            }
        }

        private void SwitchWeapon()
        {
            Weapon prevWeapon = weapon;
            for(int i = 0; i < Weapons.Count; i++)
            {
                if(Weapons[i].projectileType == weapon.projectileType)
                {
                    if (i + 1 >= Weapons.Count)
                    {
                        Weapons[i] = weapon;
                        weapon = Weapons[0];
                    }
                    else
                    {
                        Weapons[i] = weapon;
                        weapon = Weapons[i + 1];
                    }
                    break;
                }
            }
            if (prevWeapon.ammo <= 0)
                Weapons.Remove(prevWeapon);
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
            spriteBatch.DrawString(Game1.font, weapon.projectileType.Name + ": " + weapon.ammo.ToString(), new Vector2(Position.X, Position.Y + Texture.Height + 2f), Color.White);
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
            if (weapon.ammo <= 0)
            {
                SwitchWeapon();
            }
            if (!weapon.infinite)
                weapon.ammo--;
            if (weapon.projectileType == typeof(Laser))
            {
                Projectiles.Add(new Laser(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -30), Laser.DEFAULT_DMG)); //moving up
            }
            else if (weapon.projectileType == typeof(Rocket))
            {
                Projectiles.Add(new Rocket(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -30), Rocket.DEFAULT_DMG)); //moving up
            }
        }

        public bool Intersects(GameElement shot)
        {
            if (Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }

        public bool IsClicked(Vector2 input)
        {
            if (Position.X + Texture.Width >= input.X && Position.X - Texture.Width <= input.X
                && Position.Y + Texture.Height >= input.Y && Position.Y - Texture.Height <= input.Y)
                return true;
            return false;
        }

        public void Damage(int damage)
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