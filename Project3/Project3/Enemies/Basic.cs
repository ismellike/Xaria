using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Project3.Projectiles;
using System.Collections.Generic;

namespace Project3.Enemies
{
    class Basic : Enemy
    {
        public Basic(Vector2 position, uint tier)
        {
            Health = (int)(100 + 50*System.Math.Floor(tier/10d));
            Position = position;
            Tier = tier;
            NextShoot = getCooldown(tier);
            Texture = Game1.textureDictionary["basic"];
            TierColor = getTier(tier);
        }

        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if(NextShoot<= 0)
            {
                NextShoot = getCooldown(Tier);
                Projectiles.Add(new Laser(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height+ 5f), new Vector2(0, 20), 20)); //moving up
            }
        }
    }
}