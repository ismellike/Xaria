using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;
using Xaria.Drops;

namespace Xaria.Enemies
{
    class Boss1 : Enemy
    {
        private const double NEXT_TELEPORT = 3000; //3 seconds
        private const int HEALTH = 5000;
        private double Countdown = NEXT_TELEPORT;

        public Boss1(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            Texture = Game1.textureDictionary["boss1"];//texture changes
        }


        internal override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(1000, 10000);
                Projectiles.Add(new Beam(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height + 5f), new Vector2(0, 50), 200));
            }
        }

        internal override void OnDeath(ref List<Drop> drops)
        {
            base.OnDeath(ref drops);
        }

        internal override void UpdateMovement(Level level, GameTime gameTime)
        {
            Countdown -= gameTime.ElapsedGameTime.Milliseconds;
            if (Countdown <= 0)
            {
                Position = new Vector2(Level.random.Next(((int)Game1.screenSize.X - Texture.Width)), Level.random.Next((500 + Texture.Height)));
                Countdown = NEXT_TELEPORT;
            }
        }
    }
}