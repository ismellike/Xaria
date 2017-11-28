using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Xaria.Drops;
using Xaria.Projectiles;

namespace Xaria.Enemies
{
    class Advanced : Enemy
    {
        const int HEALTH = 250;
        const int FIRST = 1000;
        const int NEXT = 10000;
        const int PELLET_DMG = 10;

        public Advanced(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            NextShoot = Level.random.Next(FIRST, NEXT);
            Texture = Game1.textureDictionary["advanced"];
        }

        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if (NextShoot <= 0)
            {
                NextShoot = Level.random.Next(FIRST, NEXT);
                Projectiles.Add(new Pellet(Position + new Vector2(Texture.Width / 2f, Texture.Height + 5f), new Vector2(0, 30), PELLET_DMG)); //moving down
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
            if (Level.random.Next(20) == 1) // 1/x chance of giving drop
            {
                drops.Add(new Shield(Position + new Vector2(Texture.Width / 2f, Texture.Height), 50));
            }
        }
    }
}