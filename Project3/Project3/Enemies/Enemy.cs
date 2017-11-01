using Microsoft.Xna.Framework;

namespace Project3
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    public class Enemy : GameElement
    {
        public int Health { get; internal set; }
        public int Damage { get; internal set; }
        public Color Tier { get; internal set; }

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

        public virtual void Shoot() { } //can be inherited by enemies to shoot projectiles down
    }
}