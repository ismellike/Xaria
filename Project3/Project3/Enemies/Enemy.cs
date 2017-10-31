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
            switch (tier)
            {
                case 0:
                    return Color.White;
                case 1:
                    return Color.Yellow;
                case 2:
                    return Color.Orange;
                case 3:
                    return Color.Red;
                case 4:
                    return Color.Green;
                case 5:
                    return Color.Blue;
                case 6:
                    return Color.Indigo;
                default:
                    return Color.Pink;
            }
        }

        public virtual void Shoot() { } //can be inherited by enemies to shoot projectiles down
    }
}