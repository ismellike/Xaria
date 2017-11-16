using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;

namespace Xaria
{
    class Boss1 : Enemy
    {
        public Boss1(Vector2 position)
        {
            Health = 5000;
            Position = position;
            Texture = Game1.textureDictionary["boss1"];//texture changes
        }


        internal override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
                NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
                if (NextShoot <= 0)
                {
                    NextShoot = Level.random.Next(1000, 10000);
                    Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 20), 200));
                }
        }
    }
}