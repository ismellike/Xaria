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

namespace Xaria.Enemies
{
    abstract class Boss : Enemy
    {
        public new enum Type
        {
            Boss1 = 1,
            Boss2 = 2,
            Boss3 = 3,
            Boss4 = 4,
        }

        protected Type BossType { get; set; }
    }
}