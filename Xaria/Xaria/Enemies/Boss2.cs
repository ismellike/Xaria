using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Projectiles;

namespace Xaria.Enemies
{
    /// <summary>
    /// The boss2 boss.
    /// </summary>
    /// <seealso cref="Xaria.Enemies.Boss" />
    class Boss2 : Boss
    {
        /// <summary>
        /// Gets or sets the next shoot2.
        /// </summary>
        /// <value>
        /// The next shoot2.
        /// </value>
        private double NextShoot2 { get; set; }
        /// <summary>
        /// The health
        /// </summary>
        const int HEALTH = 6000;
        /// <summary>
        /// The firs t1
        /// </summary>
        const int FIRST1 = 1500;
        /// <summary>
        /// The firs t2
        /// </summary>
        const int FIRST2 = 1500;
        /// <summary>
        /// The nex t1
        /// </summary>
        const int NEXT1 = 10000;
        /// <summary>
        /// The nex t2
        /// </summary>
        const int NEXT2 = 3000;
        /// <summary>
        /// The rocket DMG
        /// </summary>
        const int ROCKET_DMG = 50;
        /// <summary>
        /// The emp DMG
        /// </summary>
        const int EMP_DMG = 10;

        /// <summary>
        /// Initializes a new instance of the <see cref="Boss2"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Boss2(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss2"];
            EnemyType = Enemy.Type.Boss;
            BossType = Type.Boss2;
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
                Projectiles.Add(new Emp(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 15), EMP_DMG));
            }
            NextShoot2 -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot2 <= 0)
            {
                NextShoot2 = Level.random.Next(FIRST2, NEXT2);
                Projectiles.Add(new Rocket(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 22), ROCKET_DMG));
            }
        }

        /// <summary>
        /// Updates the movement.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="gameTime">The game time.</param>
        public override void UpdateMovement(Level level, GameTime gameTime)
        {
            Position.X = level.player.Position.X + level.player.Texture.Width/2f - Texture.Width/2f;
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