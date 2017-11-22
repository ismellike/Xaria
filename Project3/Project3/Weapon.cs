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
        public Type ProjectileType { get; private set; }
        public int Ammo { get; private set; }
        public bool Infinite { get; private set; }
        public bool Immovable { get; private set; }

        public Weapon(Type projectileType, int ammo, bool infinite = false, bool immovable = false)
        {
            ProjectileType = projectileType;
            Ammo = ammo;
            Infinite = infinite;
            Immovable = immovable;
        }

        public void IncreaseAmmo(int ammo)
        {
            Ammo += ammo;
        }

        public void DecreaseAmmo()
        {
            Ammo--;
        }
    }
}