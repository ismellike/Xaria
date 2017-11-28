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

namespace Xaria.Drops
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Xaria.Drops.Drop" />
    class Shield : Drop
    {
        /// <summary>
        /// Gets the strength.
        /// </summary>
        /// <value>
        /// The strength.
        /// </value>
        public int Strength { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Shield"/> class.
        /// </summary>
        /// <param name="position">The position.</param>
        /// <param name="strength">The strength.</param>
        public Shield(Vector2 position, int strength)
        {
            Texture = Game1.textureDictionary["shield"];
            Position = position;
            Strength = strength;
        }

        /// <summary>
        /// Called when [receive].
        /// </summary>
        /// <param name="player">The player.</param>
        public override void OnReceive(ref Player player)
        {
            player.AddShield(Strength);
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
            return "Shield: " + Strength.ToString();
        }
    }
}