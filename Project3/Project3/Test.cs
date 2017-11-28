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
        private  Level level = new Level(1);
        private int difficulty = 1;
        StringBuilder testContent = new StringBuilder();
        bool runTests = true;

        public void Update(GameTime gameTime, TouchCollection touchCollection)
        {
            if (runTests)
            {
                RunTests();
                runTests = false;
            }
            CheckLevels(touchCollection);
        }

        private void RunTests()
        {
            TestPlayerDamage();
            TestPlayerAddAmmos();
            TestPlayerSwitchWeapons();
            TestEnemiesDamage();
            //testContent.AppendLine("info");
        }

        /// <summary>
        /// Tests the enemies damage.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestEnemiesDamage()
        {
            throw new NotImplementedException();
        }

        private void TestPlayerSwitchWeapons()
        {
            throw new NotImplementedException();
        }

        private void TestPlayerAddAmmos()
        {
            throw new NotImplementedException();
        }

        private void TestPlayerDamage()
        {
            throw new NotImplementedException();
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

        private void CheckLevels(TouchCollection touchCollection)
        {
            if(ShouldTest(touchCollection))
            {
                if(difficulty >= Level.FINAL_LEVEL)
                {
                    difficulty = 1;
                    level = new Level(difficulty);
                }
                difficulty++;
                level = new Level(difficulty);
            }
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            level.Draw(ref spriteBatch);
            spriteBatch.DrawString(Game1.font, testContent, new Vector2(100, 1200), Color.White);
        }

        private void Reset()
        {
            Game1.state = GameState.Start;
        }
    }
}