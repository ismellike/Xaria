using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Microsoft.Xna.Framework.Graphics;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss3 : Enemy
    {
        public double NextShoot2 { get; internal set; }

        public Boss3(Vector2 position)
        {
            Health = 5000;
            Position = position;
            Texture = Game1.textureDictionary["boss3"];//texture changes
        }


        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(200, 500);
                Projectiles.Add(new Pellet(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 15), 15));
            }
            NextShoot2 -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot2 <= 0)
            {
                NextShoot = Level.random.Next(5000, 10000);
                Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 50), 200));
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