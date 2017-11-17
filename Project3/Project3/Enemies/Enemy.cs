using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using Xaria.Drops;

namespace Xaria
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    public class Enemy : GameElement
    {
        /// <summary>
        /// Gets the health.
        /// </summary>
        /// <value>
        /// The health.
        /// </value>
        public int Health { get; internal set; }
        /// <summary>
        /// Gets the damage.
        /// </summary>
        /// <value>
        /// The damage.
        /// </value>
        public int Damage { get; internal set; }
        /// <summary>
        /// Gets the next shoot.
        /// </summary>
        /// <value>
        /// The next shoot.
        /// </value>
        public double NextShoot { get; internal set; }
        /// <summary>
        /// Gets the next shoot.
        /// </summary>
        /// <value>
        /// The next shoot.
        /// </value>
        public double NextShoot2 { get; internal set; }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString(), Position + new Vector2(10, -25), Color.Red);
            spriteBatch.Draw(Texture, Position, Color.White);
        }

        internal virtual void Shoot(GameTime gameTime, ref List<Projectile> Projectiles) {  } //can be inherited by enemies to shoot projectiles down

        //returns true if hit so the projectile can be deleted
        public bool IsHit(Projectile shot)
        {
            if(Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }

        internal virtual void OnDeath(ref List<Drop> drops)
        {
        }
    }
}