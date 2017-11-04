using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Xaria.Projectiles;

namespace Xaria.Screens
{
    /// <summary>
    /// The background class
    /// </summary>
    class Background
    {
        /// <summary>
        /// The stars
        /// </summary>
        List<Star> stars = new List<Star>();
        /// <summary>
        /// The next star cooldown
        /// </summary>
        public double nextStar;
        /// <summary>
        /// The random class
        /// </summary>
        Random random;

        /// <summary>
        /// Initializes a new instance of the <see cref="Background" /> class.
        /// </summary>
        /// <param name="seed">The seed.</param>
        public Background(int seed)
        {
            random = new Random(seed);
            nextStar = random.Next(200, 1500);
        }

        /// <summary>
        /// Draws the stars.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (Star star in stars)
                star.Draw(ref spriteBatch);
        }

        /// <summary>
        /// Adds and moves stars with a random velocity.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        public void Update(GameTime gameTime)
        {
            for (int i = stars.Count - 1; i >= 0; i--)
            {
                stars[i].Position += stars[i].Velocity;
                if (stars[i].Position.Y >= Game1.screenSize.Y)
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