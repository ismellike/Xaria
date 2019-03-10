using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

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