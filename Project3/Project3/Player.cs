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
    /// <seealso cref="Xaria.GameElement" />
    public class Player : GameElement
    {
        /// <summary>
        /// Gets the extra lives.
        /// </summary>
        /// <value>
        /// The extra lives.
        /// </value>
        public int ExtraLives { get; private set; }
        /// <summary>
        /// Gets the health.
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
        public int Health { get; private set; }
        /// <summary>
        /// The weapons
        /// </summary>
        private List<Weapon> Weapons = new List<Weapon>() { new Weapon(Projectile.Type.Laser, 1, true) };
        /// <summary>
        /// The weapon
        /// </summary>
        internal Weapon weapon = new Weapon(Projectile.Type.Laser, 1, true);
        /// <summary>
        /// The projectiles
        /// </summary>
        private List<Projectile> Projectiles = new List<Projectile>();
        /// <summary>
        /// The starting health
        /// </summary>
        public const int STARTING_HEALTH = 100;
        /// <summary>
        /// Gets the shield.
        /// </summary>
        /// <value>
        /// The shield.
        /// </value>
        public int Shield { get; private set; }
        /// <summary>
        /// Gets the stunned.
        /// </summary>
        /// <value>
        /// The stunned.
        /// </value>
        public double Stunned { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Player" /> class.
        /// </summary>
        public Player()
        {
            Health = STARTING_HEALTH;
            Texture = Game1.textureDictionary["ship"];
            Position = new Vector2((Game1.screenSize.X + Texture.Width)/ 2f, Game1.screenSize.Y - Texture.Height - 25);
            ExtraLives = 0;
            Stunned = -1;
        }

        /// <summary>
        /// Adds the stun.
        /// </summary>
        /// <param name="StunDuration">Duration of the stun.</param>
        public void AddStun(int StunDuration)
        {
            Stunned += StunDuration;
        }


        /// <summary>
        /// Adds the life.
        /// </summary>
        public void AddLife()
        {
            ExtraLives++;
        }

        /// <summary>
        /// Adds the shield.
        /// </summary>
        /// <param name="ShieldStrength">The shield strength.</param>
        public void AddShield(int ShieldStrength)
        {
            Shield += ShieldStrength;
        }

        /// <summary>
        /// Updates the specified touch and checks for player projectile collision with enemies.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="touches">The touches.</param>
        /// <param name="roll">The roll.</param>
        /// <param name="Enemies">The enemies.</param>
        internal void Update(GameTime gameTime, TouchLocation[] touches, float roll, ref List<List<Enemy>> Enemies)
        {
            if (Stunned >= 0)
            {
                Stunned -= gameTime.ElapsedGameTime.Milliseconds;
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

            UpdateProjectiles(ref Enemies);
        }

        /// <summary>
        /// Updates the projectiles.
        /// </summary>
        /// <param name="Enemies">The enemies.</param>
        private void UpdateProjectiles(ref List<List<Enemy>> Enemies)
        {
            for (int projectileIndex = Projectiles.Count - 1; projectileIndex >= 0; projectileIndex--)
            {
                Projectile projectile = Projectiles[projectileIndex];
                if (projectile.Position.X <= 0)
                {
                    Projectiles.RemoveAt(projectileIndex);
                    continue;
                }
                projectile.Move();

                for (int y = Enemies.Count - 1; y >= 0; y--)
                    for (int x = Enemies[y].Count - 1; x >= 0; x--) //lower rows are more likely to be hit by a projecitle
                    {
                        if (Enemies[y][x].IsHit(projectile))
                        {
                            projectile.OnCollision(ref Enemies, y, x);
                            if (!projectile.IsImmovable())
                                Projectiles.RemoveAt(projectileIndex);
                            goto exit;
                        }
                    }
                exit:;
            }
        }

        /// <summary>
        /// Switches the weapon.
        /// </summary>
        public void SwitchWeapon()
        {
            Weapon prevWeapon = weapon;
            for(int i = 0; i < Weapons.Count; i++)
            {
                if(Weapons[i].ProjectileType == weapon.ProjectileType)
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
            if (prevWeapon.Ammo <= 0)
                Weapons.Remove(prevWeapon);
        }

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
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
            spriteBatch.DrawString(Game1.font, weapon.ProjectileType.ToString() + ": " + weapon.Ammo.ToString(), new Vector2(Position.X, Position.Y + Texture.Height + 2f), Color.White);
        }

        /// <summary>
        /// Shoots a projectile after a given time.
        /// </summary>
        internal void Shoot()
        {
            if (!weapon.Infinite)
                weapon.DecreaseAmmo();

            switch (weapon.ProjectileType) {
                case Projectile.Type.Laser:
                Projectiles.Add(new Laser(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -30), Laser.DEFAULT_DMG)); //moving up
                    break;
                case Projectile.Type.Rocket:
                    Projectiles.Add(new Rocket(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -30), Rocket.DEFAULT_DMG)); //moving up
                    break;
                case Projectile.Type.Beam:
                    Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, -5f), new Vector2(0, -50), Beam.DEFAULT_DMG));
                    break;
            }
            if (weapon.Ammo <= 0)
                SwitchWeapon();
        }

        /// <summary>
        /// Intersectses the specified shot.
        /// </summary>
        /// <param name="shot">The shot.</param>
        /// <returns></returns>
        public bool Intersects(GameElement shot)
        {
            if (Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether the specified input is clicked.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is clicked; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClicked(Vector2 input)
        {
            if (Position.X + Texture.Width >= input.X && Position.X - Texture.Width <= input.X
                && Position.Y + Texture.Height >= input.Y && Position.Y - Texture.Height <= input.Y)
                return true;
            return false;
        }

        /// <summary>
        /// Damages the specified damage.
        /// </summary>
        /// <param name="damage">The damage.</param>
        public void Damage(int damage)
        {
            if (Game1.state == GameState.Testing)
                return;
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
                    Health = STARTING_HEALTH;
                }
            }
        }


        /// <summary>
        /// Increases the ammo.
        /// </summary>
        /// <param name="projectileType">Type of the projectile.</param>
        /// <param name="ammo">The ammo.</param>
        /// <param name="immovable">if set to <c>true</c> [immovable].</param>
        internal void IncreaseAmmo(Projectile.Type projectileType, int ammo, bool immovable = false)
        {
            for (int i = 0; i < Weapons.Count; i++)
            {
                if (Weapons[i].ProjectileType == projectileType)
                {
                    Weapons[i].IncreaseAmmo(ammo);
                    return;
                }
            }
            Weapons.Add(new Weapon(projectileType, ammo, false, immovable));
        }
    }
}