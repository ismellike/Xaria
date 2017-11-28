using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;
using Android.Media;
using System;

namespace Xaria
{
    /// <summary>
    /// The screen class
    /// </summary>
    class Screen
    {
        /// <summary>
        /// The buttons
        /// </summary>
        public List<Button> Buttons = new List<Button>();
        /// <summary>
        /// Draws the buttons.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        internal virtual void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (Button button in Buttons)
                button.Draw(ref spriteBatch);
        }

        public MediaPlayer mediaPlayer = null;
        public string MP3 = "game.mp3";
        public void StartPlayer()
        {
            try
            {
                String sdCard = "/sdcard/";//storage/emulated/0
                mediaPlayer.Reset();
                mediaPlayer.SetDataSource(sdCard + MP3);
                mediaPlayer.Prepare();//prepare music
                mediaPlayer.Start();//start music
            }
            catch
            {

            }

        }

        internal void Start()
        {
            try
            {
                String sdCard = "/sdcard/";
                mediaPlayer.Reset();
                mediaPlayer.SetDataSource(sdCard + MP3);
                mediaPlayer.Prepare();
                mediaPlayer.Start();
            }
            catch
            {

            }
        }
        internal void Stop()
        {
            try
            {
                mediaPlayer.Stop();//stop music
            }
            catch
            {

            }
        }
        /// <summary>
        /// Updates the specified touch to see if buttons are clicked.
        /// </summary>
        /// <param name="touches">The touches.</param>
        /// <param name="window">The window.</param>
        internal void Update(TouchLocation[] touches, GameWindow window)
        {
            foreach (TouchLocation touch in touches)
            {
                Buttons.ForEach((Button button) =>
                {
                    if (button.IsClicked(touch))
                    {
                        button.Click();
                    }
                });
            }
        }
    }
}