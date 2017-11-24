using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss2 : Enemy
    {
        public double NextShoot2 { get; internal set; }

        public Boss2(Vector2 position)
        {
            Health = 5000;
            Position = position;
            Texture = Game1.textureDictionary["boss2"];
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
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(1000, 3000);
                Projectiles.Add(new Rocket(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 22), 60));
            }
        }

        public override void UpdateMovement(Level level, GameTime gameTime)
        {
            throw new global::System.NotImplementedException();
        }

        internal override void OnDeath(ref List<Drop> drops)
        {
            throw new System.NotImplementedException();
        }
    }
}