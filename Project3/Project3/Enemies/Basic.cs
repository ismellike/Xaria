using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Project3.Enemies
{
    class Basic : Enemy
    {
        public Basic(Vector2 position, uint tier)
        {
            Health = (int)(100 + 50*tier);
            Position = position;
            ShootCooldown = NextShoot = getCooldown(tier);
            Texture = Game1.textureDictionary["basic"];
            Tier = getTier(tier);
            hitBox = new Rectangle((int)position.X, (int)position.Y, Texture.Width, Texture.Height);
        }

        public override void Shoot(GameTime gameTime)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if(NextShoot<= 0)
            {
                //add projectile
                NextShoot = ShootCooldown;
            }
        }
    }
}