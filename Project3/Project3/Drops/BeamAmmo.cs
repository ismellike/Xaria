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
    class BeamAmmo : Drop
    {
        public int amount = 0;

        public BeamAmmo(Vector2 position, int Amount)
        {
            Position = position;
            amount = Amount;
            Texture = Game1.textureDictionary["beamAmmo"];
        }

        public override void OnReceive(ref Player player)
        {
            player.IncreaseAmmo(typeof(Beam), amount);
        }

        public override string ToString()
        {
            return "Beam Ammo: " + amount.ToString();
        }
    }
}