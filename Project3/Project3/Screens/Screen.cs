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
        /// <param name="touch">The touch.</param>
        internal void Update(TouchCollection touches)
        {
            if (touches.Count > 0)
                Buttons.ForEach((Button button) =>
                {
                    if (button.IsClicked(touches[0].Position))
                    {
                        button.Click();
                    }
                });
        }
    }
}