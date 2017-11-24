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
using static Xaria.Player;
using Xaria.Projectiles;

namespace Xaria.Drops
{
    class RocketAmmo : Drop
    {
        public int amount = 0;

        public RocketAmmo(Vector2 position, int Amount)
        {
            Position = position;
            amount = Amount;
            Texture = Game1.textureDictionary["rocketAmmo"];
        }

        public override void OnReceive(ref Player player)
        {
            player.IncreaseAmmo(typeof(Rocket), amount);
        }

        public override string ToString()
        {
            return "Rocket Ammo: " + amount.ToString();
        }
    }
}