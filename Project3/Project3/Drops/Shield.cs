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
    class Shield : Drop
    {
        public int Strength { get; private set; }

        public Shield(Vector2 position, int strength)
        {
            Texture = Game1.textureDictionary["shield"];
            Position = position;
            Strength = strength;
        }

        public override void OnReceive(ref Player player)
        {
            player.AddShield(Strength);
        }

        public override string ToString()
        {
            return "Shield: " + Strength.ToString();
        }
    }
}