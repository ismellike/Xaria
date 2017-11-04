using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Xaria.Projectiles;

namespace Xaria.Enemies
{
    /// <summary>
    /// The basic enemy class.
    /// </summary>
    /// <seealso cref="Xaria.Enemy" />
    class Basic : Enemy
    {
        /// <summary>
        /// The health
        /// </summary>
        const int HEALTH = 100;

        /// <summary>
        /// Initializes a new instance of the <see cref="Basic"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        public Basic(Vector2 position)
        {
            Health = HEALTH;
            Position = position;
            NextShoot = Level.random.Next(1000, 10000);
            Texture = Game1.textureDictionary["basic"];
        }

        /// <summary>
        /// Shoots a projectile from the enemy.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="Projectiles">The projectiles.</param>
        public override void Shoot(GameTime gameTime, ref List<Projectile> Projectiles)
        {
            NextShoot -= gameTime.ElapsedGameTime.Milliseconds;
            if(NextShoot<= 0)
            {
                NextShoot = Level.random.Next(1000, 10000);
                Projectiles.Add(new Laser(Position + new Vector2(Texture.Width / 2f - 1f, Texture.Height+ 5f), new Vector2(0, 20), 20)); //moving up
            }
        }
    }
}