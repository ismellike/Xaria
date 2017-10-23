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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Project3
{
    class Enemy
    {
        public Texture2D Texture { get; private set; }
        public Vector2 Position { get; private set; }
        public int Health { get; private set; }
    }
}