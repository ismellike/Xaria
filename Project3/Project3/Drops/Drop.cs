﻿using Microsoft.Xna.Framework;

namespace Xaria.Drops
{
    public class Drop : GameElement
    {
        //make drop textures around 32x32
        public const float DROP_SPEED = 10f;

        public readonly Vector2 Velocity = new Vector2(0, DROP_SPEED);

        public virtual void OnReceive(ref Player player)
        {
        }
    }
}