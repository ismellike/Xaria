using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Drops;
using Xaria.Projectiles;

namespace Xaria.Enemies
{
    /// <summary>
    /// The boss1 boss.
    /// </summary>
    /// <seealso cref="Xaria.Enemies.Boss" />
    class Boss1 : Boss
    {
        /// <summary>
        /// The next teleport
        /// </summary>
        private const double NEXT_TELEPORT = 4000; //4 seconds
        /// <summary>
        /// The shoot
        /// </summary>
        private const double SHOOT = 1500; //1.5 seconds after teleporting shoot
        /// <summary>
        /// The health
        /// </summary>
        private const int HEALTH = 5000;
        /// <summary>
        /// The beam DMG
        /// </summary>
        const int BEAM_DMG = 100;
        /// <summary>
        /// The beam ammo
        /// </summary>
        const int BEAM_AMMO = 5;
        /// <summary>
        /// The countdown
        /// </summary>
        private double Countdown = NEXT_TELEPORT;
        /// <summary>
        /// The can shoot
        /// </summary>
        private bool CanShoot = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="Boss1"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Boss1(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss1"]; //texture changes
            NextShoot = SHOOT;
            EnemyType = Enemy.Type.Boss;
            BossType = Type.Boss1;
        }

        /// <summary>
        /// Shoots the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Projectiles">The projectiles.</param>
        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            if (CanShoot)
            {
                NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
                if (NextShoot <= 0)
                {
                    NextShoot = SHOOT;
                    Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f), new Vector2(0, 50), BEAM_DMG, false));
                    CanShoot = false;
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
                drops.Add(new BeamAmmo(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f), BEAM_AMMO));
            }
        }

        /// <summary>
        /// Updates the movement.
        /// </summary>
        /// <param name="level">The level.</param>
        /// <param name="gameTime">The game time.</param>
        public override void UpdateMovement(Level level, GameTime gameTime)
        {
            Countdown -= gameTime.ElapsedGameTime.Milliseconds;
            if (Countdown <= 0)
            {
                Position = new Vector2(Level.random.Next(((int)Game1.screenSize.X - Texture.Width)), Position.Y);
                Countdown = NEXT_TELEPORT;
                CanShoot = true;
            }
        }
    }
}