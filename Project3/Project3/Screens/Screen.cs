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
        /// <param name="touch">The touch.</param>
        internal void Update(TouchCollection touches, GameWindow window)
        {

            if (touches.Count > 0)
            {
                TouchLocation scaled = new TouchLocation(touches[0].Id, touches[0].State, touches[0].Position * Game1.screenSize / new Vector2(window.ClientBounds.Width, window.ClientBounds.Height));

                Buttons.ForEach((Button button) =>
                {
                    if (button.IsClicked(scaled.Position))
                    {
                        button.Click();
                    }
                });
            }
        }
    }
}