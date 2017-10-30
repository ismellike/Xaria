namespace Project3
{
    /// <summary>
    /// Generic enemy class inherited by specific enemies
    /// </summary>
    public class Enemy : GameElement
    {
        public int Health { get; internal set; }

        public virtual void Shoot() { } //can be inherited by enemies to shoot projectiles down
    }
}