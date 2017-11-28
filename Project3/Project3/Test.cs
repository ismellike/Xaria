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
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Xaria
{
    class Test
    {
        enum State
        {
            Levels,
            Projectiles,
            Input
        }
        private  Level level = new Level(1);
        private int difficulty = 1;
        private string Display = "Testing Level Generation for Level 1";
        State state = State.Levels;

        public void Update(TouchCollection touchCollection)
        {
            switch (state)
            {
                case State.Levels:
                    CheckLevels(touchCollection);
                    break;
                case State.Projectiles:
                    CheckProjectiles(touchCollection);
                    break;
                case State.Input:
                    CheckInput(touchCollection);
                    break;
            }
        }

        private void CheckInput(TouchCollection touchCollection)
        {
        }

        bool ShouldTest(TouchCollection touchCollection)
        {
            foreach(TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Released)
                    return true;
            }
            return false;
        }

        private void CheckProjectiles(TouchCollection touchCollection)
        {
            if (ShouldTest(touchCollection))
            {
                switch (level.player.weapon.ProjectileType)
                {
                    case Projectile.Type.Laser:
                        level.player.Shoot();
                        state = State.Input;
                        break;
                    case Projectile.Type.Beam:
                        level.player.IncreaseAmmo(Projectile.Type.Rocket, 1);
                        level.player.Shoot();
                        level.player.SwitchWeapon();
                        break;
                    case Projectile.Type.Rocket:
                        level.player.IncreaseAmmo(Projectile.Type.Laser, 1, true);
                        level.player.Shoot();
                        level.player.SwitchWeapon();
                        break;
                }
                Display = "Testing shooting " + level.player.weapon.ProjectileType.ToString() + " projectile.";
            }
        }

        private void CheckLevels(TouchCollection touchCollection)
        {
            if(ShouldTest(touchCollection))
            {
                if(difficulty >= Level.FINAL_LEVEL)
                {
                    state = State.Projectiles;
                    difficulty = 1;
                    level = new Level(difficulty);
                    Display = "Testing shooting Beam projectile";
                }
                difficulty++;
                level = new Level(difficulty);
                Display = "Testing Level Generation for Level: " + difficulty.ToString();
            }
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            level.Draw(ref spriteBatch);
            spriteBatch.DrawString(Game1.font, Display, new Vector2(100, 700), Color.White);
        }

        private void Reset()
        {
            Game1.state = GameState.Start;
        }
    }
}