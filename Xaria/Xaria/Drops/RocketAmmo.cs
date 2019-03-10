using Microsoft.Xna.Framework;

namespace Xaria.Drops
{
    /// <summary>
    /// The rocket ammo drop class
    /// </summary>
    /// <seealso cref="Xaria.Drops.Drop" />
    class RocketAmmo : Drop
    {
        /// <summary>
        /// The amount
        /// </summary>
        public int amount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="RocketAmmo"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="Amount">The amount.</param>
        public RocketAmmo(Vector2 position, int Amount)
        {
            Position = position;
            amount = Amount;
            Texture = Game1.textureDictionary["rocketAmmo"];
        }

        /// <summary>
        /// Called when [receive].
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnReceive(ref Player player)
        {
            player.IncreaseAmmo(Projectile.Type.Rocket, amount);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        /// <remarks>
        /// To be added.
        /// </remarks>
        public override string ToString()
        {
            return "Rocket Ammo: " + amount.ToString();
        }
    }
}