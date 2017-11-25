using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss4 : Enemy
    {
        private double NextShoot2;
        private double NextShoot3;
        private double NextShoot4;

        public Boss4(Vector2 position)
        {
            Health = 10000;
            Position = position;
            Texture = Game1.textureDictionary["boss4"];//texture changes
        }


        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(3000, 10000);
                Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 50), 200));
            }
            NextShoot2 -= gameTime.ElapsedGameTime.Milliseconds;
            if(Health <= 5000)
            {
                if (NextShoot2 <= 0)
                {
                    NextShoot2 = Level.random.Next(5000, 10000);
                    Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 50), 200));
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
                Projectiles.Add(new Emp(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 15), 10));
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
                Projectiles.Add(new Rocket(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 22), 60));
            }
        }

        public override void UpdateMovement(Level level, GameTime gameTime)
        {
            throw new System.NotImplementedException();
        }

        internal override void OnDeath(ref List<Drop> drops)
        {
            throw new System.NotImplementedException();
        }
    }
}