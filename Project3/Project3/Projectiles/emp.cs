using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The Emp class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Emp : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Laser" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Emp(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["emp"];
            Damage = damage;
            ProjectileType = Type.Emp;
        }

        /* @Pre: emp projectile overlaps player sprite
         * @Post: players health is reduced by the amount of damage the emp does. Player cannot move for three seconds.
         * @Return: None
         */
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
            player.AddStun(3000); //stunned variable in player.cs set to 3000 milliseconds, player cannot do anything during that time.
        }

        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            throw new System.NotImplementedException();
        }

        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}