using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Project3.Projectiles;
using Microsoft.Xna.Framework;

namespace Project3.Screens
{
    class Background
    {
        List<Star> stars = new List<Star>();
        public double nextStar;
        Random random;

        public Background(int seed)
        {
            random = new Random(seed);
            nextStar=  random.Next(200, 1500);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            stars.ForEach((Star star) =>
            {
                spriteBatch.Draw(star.Texture, star.Position, null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            });
        }

        public void Update(GameTime gameTime)
        {
            for(int i = stars.Count - 1; i >= 0; i--)
            {
                stars[i].Position += stars[i].Velocity;
                if(stars[i].Position.Y >= Game1.screenSize.Y)
                {
                    stars.RemoveAt(i);
                }
            }
            nextStar -= gameTime.ElapsedGameTime.Milliseconds;
            if (nextStar <= 0)
            {
                stars.Add(new Star(new Vector2(random.Next((int)Game1.screenSize.X), 0), new Vector2(0, random.Next(1, 20))));
                nextStar = random.Next(200, 1500);
            }
        }
    }
}