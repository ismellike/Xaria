using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Projectiles;

namespace Xaria.Enemies
{
    /// <summary>
    /// The boss3 boss.
    /// </summary>
    /// <seealso cref="Xaria.Enemies.Boss" />
    class Boss3 : Boss
    {
        /// <summary>
        /// Gets or sets the next shoot2.
        /// </summary>
        /// <value>
        /// The next shoot2.
        /// </value>
        public double NextShoot2 { get; internal set; }
        /// <summary>
        /// The health
        /// </summary>
        const int HEALTH = 5000;
        /// <summary>
        /// The firs t1
        /// </summary>
        const int FIRST1 = 500;
        /// <summary>
        /// The firs t2
        /// </summary>
        const int FIRST2 = 5000;
        /// <summary>
        /// The nex t1
        /// </summary>
        const int NEXT1 = 1000;
        /// <summary>
        /// The nex t2
        /// </summary>
        const int NEXT2 = 10000;
        /// <summary>
        /// The pellet DMG
        /// </summary>
        const int PELLET_DMG = 15;
        /// <summary>
        /// The laser DMG
        /// </summary>
        const int LASER_DMG = 150;

        /// <summary>
        /// Initializes a new instance of the <see cref="Boss3"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Boss3(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss3"];//texture changes
            NextShoot2 = Level.random.Next(FIRST2, NEXT2);
            EnemyType = Enemy.Type.Boss;
            BossType = Type.Boss3;
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
                NextShoot = Level.random.Next(FIRST1, NEXT1);
                Projectiles.Add(new Pellet(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 15), PELLET_DMG));
            }
            NextShoot2 -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot2 <= 0)
            {
                NextShoot2 = Level.random.Next(FIRST2, NEXT2);
                Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 50), LASER_DMG));
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