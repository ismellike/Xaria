using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    public class Enemy : GameElement
    {
        public int Health { get; internal set; }
        public int Damage { get; internal set; }
        public Color TierColor { get; internal set; }
        public uint Tier { get; internal set; }
        public double NextShoot { get; internal set; }
        public Rectangle hitBox { get; internal set; }

        internal double getCooldown(uint tier)
        {
            double cooldown = 10000 - Tier * 500;
            if (cooldown <= 1000)
                return 1000d;
            return Level.random.Next(1000, (int)cooldown);
        }

        public Color getTier(uint tier)
        {
                if(tier<=10)
                    return Color.White;
            if (tier <= 20)
                return Color.Yellow;
            if (tier <= 30)
                return Color.Orange;
            if (tier <= 40)
                return Color.Red;
            if (tier <= 50)
                return Color.Green;
            if (tier <= 60)
                return Color.Blue;
            if (tier <= 70)
                return Color.Indigo;
             return Color.Pink;
        }

        public override void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(Game1.font, Health.ToString(), Position + new Vector2(10, -25), Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            spriteBatch.Draw(Texture, Position, null, TierColor, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
        }

        public virtual void Shoot(GameTime gameTime, ref List<Projectile> Projectiles) {  } //can be inherited by enemies to shoot projectiles down

        //returns true if hit so the projectile can be deleted
        public bool isHit(Projectile shot)
        {
            if(Bounds().Intersects(shot.Bounds()))
                return true;
            return false;
        }
    }
}