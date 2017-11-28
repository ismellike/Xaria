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
    /// <summary>
    /// 
    /// </summary>
    class Test
    {
        /// <summary>
        /// The level
        /// </summary>
        private Level level = new Level(1);
        /// <summary>
        /// The difficulty
        /// </summary>
        private int difficulty = 1;
        /// <summary>
        /// The test content
        /// </summary>
        StringBuilder testContent = new StringBuilder();
        /// <summary>
        /// The run tests
        /// </summary>
        bool runTests = true;

        /// <summary>
        /// Updates the specified game time.
        /// </summary>
        /// <param name="gameTime">The game time.</param>
        /// <param name="touchCollection">The touch collection.</param>
        public void Update(GameTime gameTime, TouchCollection touchCollection)
        {
            if (runTests)
            {
                RunTests();
                runTests = false;
            }
            CheckLevels(touchCollection);
        }

        /// <summary>
        /// Runs the tests.
        /// </summary>
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

        /// <summary>
        /// Tests the player switch weapons.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestPlayerSwitchWeapons()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the player add ammos.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestPlayerAddAmmos()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Tests the player damage.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void TestPlayerDamage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shoulds the test.
        /// </summary>
        /// <param name="touchCollection">The touch collection.</param>
        /// <returns></returns>
        bool ShouldTest(TouchCollection touchCollection)
        {
            foreach(TouchLocation tl in touchCollection)
            {
                if (tl.State == TouchLocationState.Released)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks the levels.
        /// </summary>
        /// <param name="touchCollection">The touch collection.</param>
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

        /// <summary>
        /// Draws the specified sprite batch.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        public void Draw(ref SpriteBatch spriteBatch)
        {
            level.Draw(ref spriteBatch);
            spriteBatch.DrawString(Game1.font, testContent, new Vector2(100, 1200), Color.White);
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        private void Reset()
        {
            Game1.state = GameState.Start;
        }
    }
}