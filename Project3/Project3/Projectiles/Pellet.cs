using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace Xaria.Projectiles
{
    /// <summary>
    /// The pellet projectile class.
    /// </summary>
    /// <seealso cref="Xaria.Projectile" />
    class Pellet : Projectile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Laser" /> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="velocity">The velocity.</param>
        /// <param name="damage">The damage.</param>
        public Pellet(Vector2 position, Vector2 velocity, int damage)
        {
            Position = position;
            Velocity = velocity;
            Texture = Game1.textureDictionary["pellet"];
            Damage = damage;
            ProjectileType = Type.Pellet;
        }

        /// <summary>
        /// @Pre: pellet projectile overlaps enemy sprite
        /// @Post: Enemy's health is reduced by laser's damage amount
        /// @Return: None
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnCollision(ref Player player)
        {
            player.Damage(Damage);
        }

        /// <summary>
        /// Called when [collision].
        /// </summary>
        /// <param name="Enemies">The enemies.</param>
        /// <param name="y">The y.</param>
        /// <param name="x">The x.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        public override void OnCollision(ref List<List<Enemy>> Enemies, int y, int x)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Draws from enemy.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public override void DrawFromEnemy(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Position, Color.White);
        }
    }
}