using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;

namespace Xaria
{
    class Boss
    {
        public Boss(Vector2 position, int bossLvl)
        {
            Health = 5000 + 5000*(bossLvl/4);
            Position = position;
            Texture = Game1.textureDictionary["boss" + bossLvl];//texture changes
        }


        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles, int bossLvl)
        {
            if(bossLvl==1)
            {
                NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
                if (NextShoot <= 0)
                {
                    NextShoot = Level.random.Next(1000, 10000);
                    Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 20), 200));
                }
            }
            else
            {

            }
        }
    }
}