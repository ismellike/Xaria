using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Projectiles;

namespace Xaria.Enemies
{
    /// <summary>
    /// The boss4 boss.
    /// </summary>
    /// <seealso cref="Xaria.Enemies.Boss" />
    class Boss4 : Boss
    {
        /// <summary>
        /// The next shoot2
        /// </summary>
        private double NextShoot2 = 0;
        /// <summary>
        /// The next shoot3
        /// </summary>
        private double NextShoot3 = 10000;
        /// <summary>
        /// The next shoot4
        /// </summary>
        private double NextShoot4 = 20000;
        /// <summary>
        /// The health
        /// </summary>
        const int HEALTH = 10000;

        /// <summary>
        /// Initializes a new instance of the <see cref="Boss4"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Boss4(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss4"];
            NextShoot = 5000;
            EnemyType = Enemy.Type.Boss;
            BossType = Type.Boss4;
        }

        /// <summary>
        /// Shoots the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Projectiles">The projectiles.</param>
        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(3000, 10000);
                Projectiles.Add(new Beam(Position + new Vector2((Texture.Width / 2f) - 1f, Texture.Height + 5f), new Vector2(0, 50), 200));
            }
            NextShoot2 -= gameTime.ElapsedGameTime.Milliseconds;
            if(Health <= 5000)
            {
                if (NextShoot2 <= 0)
                {
                    NextShoot2 = Level.random.Next(5000, 10000);
                    Projectiles.Add(new Beam(Position + new Vector2((Texture.Width / 2f) - 1f, Texture.Height + 5f), new Vector2(0, 50), 200));
                }
            }
            NextShoot3 -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot3 <= 0)
            {
                if(Health <= 2000)
                {
                    NextShoot3 = 4000;
                }
                else
                {
                    NextShoot3 = 10000;
                }
                Projectiles.Add(new Emp(Position + new Vector2((Texture.Width / 2f) - 1f, Texture.Height + 5f), new Vector2(0, 15), 10));
            }
            NextShoot4 -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot4 <= 0)
            {
                if (Health <= 2000)
                {
                    NextShoot4 = 12000;
                }
                else
                {
                    NextShoot4 = 20000;
                }
                Projectiles.Add(new Rocket(Position + new Vector2((Texture.Width / 2f) - 1f, Texture.Height + 5f), new Vector2(0, 22), 60));
            }
        }

        /// <summary>
        /// Updates the movement.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="gameTime">The game time.</param>
        public override void UpdateMovement(Level level, GameTime gameTime)
        {
            if (level.movingRight)
            {
                Position.X += 1;
                if (Position.X + Texture.Width >= Game1.screenSize.X)
                {
                    level.movingRight = !level.movingRight;
                    level.MoveDown();
                }
            }
            else
            {
                Position.X -= 1;
                if (Position.X <= 0)
                {
                    level.movingRight = !level.movingRight;
                    level.MoveDown();
                }
            }
        }

        /// <summary>
        /// Called when [death].
        /// </summary>
        /// <param name="drops">The drops.</param>
        internal override void OnDeath(ref List<Drop> drops)
        {
            drops.Add(new Life(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f)));
            if (Level.random.Next(2) == 1) //1/2 chance to drop
            {
                drops.Add(new BeamAmmo(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f), 5));
            }
        }
    }
}