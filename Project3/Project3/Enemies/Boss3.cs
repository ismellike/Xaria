using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss3 : Boss
    {
        public double NextShoot2 { get; internal set; }
        const int HEALTH = 5000;
        const int FIRST1 = 500;
        const int FIRST2 = 5000;
        const int NEXT1 = 1000;
        const int NEXT2 = 10000;
        const int PELLET_DMG = 15;
        const int LASER_DMG = 150;

        public Boss3(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss3"];//texture changes
            EnemyType = Enemy.Type.Boss;
            BossType = Type.Boss3;
        }

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