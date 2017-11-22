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

namespace Xaria
{
    class Weapon
    {
        public Type ProjectileType { get; internal set; }
        public int Ammo { get; internal set; }
        public bool Infinite { get; internal set; }

        public Weapon(Type projectileType, int ammo, bool infinite = false)
        {
            ProjectileType = projectileType;
            Ammo = ammo;
            Infinite = infinite;
        }
    }
}