using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Xaria
{
    /// <summary>
    /// The button class
    /// </summary>
    /// <seealso cref="Xaria.GameElement" />
    class Button : GameElement
    {
        /// <summary>
        /// The click action
        /// </summary>
        Action clickAction;

        /// <summary>
        /// Initializes a new instance of the <see cref="Button" /> class.
        /// </summary>
        /// <param name="texture">The texture.</param>
        /// <param name="position">The position.</param>
        /// <param name="action">The action.</param>
        public Button(Texture2D texture, Vector2 position, Action action)
        {
            Texture = texture;
            Position = position;
            clickAction = action;
        }

        /// <summary>
        /// Determines whether the specified input is clicked.
        /// </summary>
        /// <param name="input">The input.</param>
        /// <returns>
        ///   <c>true</c> if the specified input is clicked; otherwise, <c>false</c>.
        /// </returns>
        public bool IsClicked(Vector2 input)
        {
            if (Position.X + Texture.Width >= input.X && Position.X - Texture.Width <= input.X
                && Position.Y + Texture.Height >= input.Y && Position.Y - Texture.Height <= input.Y)
                return true;
            return false;
        }

        /// <summary>
        /// Invokes the click action
        /// </summary>
        public void Click()
        {
            clickAction.Invoke();
        }
    }
}