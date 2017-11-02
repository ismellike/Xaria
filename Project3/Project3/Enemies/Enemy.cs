using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Project3
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    public class Enemy : GameElement
    {
        private Random random = new Random();
        public int Health { get; internal set; }
        public List<Projectile> Projectiles { get; internal set; }
        public int Damage { get; internal set; }
        public Color Tier { get; internal set; }
        public double ShootCooldown { get; internal set; } //in milliseconds
        public double NextShoot { get; internal set; }
        public Rectangle hitBox { get; internal set; }

        internal double getCooldown(uint tier)
        {
            double cooldown = 10000 - tier * 500;
            if (cooldown <= 1000)
                return 1000d;
            return random.Next(1000, (int)cooldown);
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

        public virtual void Shoot(GameTime gameTime) { } //can be inherited by enemies to shoot projectiles down

        //returns true if hit so the projectile can be deleted

        private void lowerHealth(int damage)
        {
            Health -= damage;
        }
    }
}