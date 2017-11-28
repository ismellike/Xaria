using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss2 : Boss
    {
        private double NextShoot2 { get; set; }
        const int HEALTH = 6000;
        const int FIRSTSHOOT = 1000;

        public Boss2(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss2"];
            EnemyType = Enemy.Type.Boss;
            BossType = Type.Boss2;
        }

        private bool Spawned = false;
        public bool SpawnMoreEnemies()
        {
            return !Spawned && Health < 2500;
        }

        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(1000, 10000);
                Projectiles.Add(new Emp(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 15), 10));
            }
            NextShoot2 -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot2 <= 0)
            {
                NextShoot2 = Level.random.Next(1000, 3000);
                Projectiles.Add(new Rocket(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 22), 60));
            }
        }

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