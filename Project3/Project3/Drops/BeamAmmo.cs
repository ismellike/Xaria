using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.Xna.Framework;
using Xaria.Projectiles;

namespace Xaria.Drops
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xaria.Drops.Drop" />
    class BeamAmmo : Drop
    {
        /// <summary>
        /// The amount
        /// </summary>
        public int amount = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="BeamAmmo"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="Amount">The amount.</param>
        public BeamAmmo(Vector2 position, int Amount)
        {
            Position = position;
            amount = Amount;
            Texture = Game1.textureDictionary["beamAmmo"];
        }

        /// <summary>
        /// Called when [receive].
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnReceive(ref Player player)
        {
            player.IncreaseAmmo(Projectile.Type.Beam, amount);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Beam Ammo: " + amount.ToString();
        }
    }
}