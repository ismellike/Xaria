using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Project3
{
    class Button : GameElement
    {
        Action clickAction;
        public Button(Texture2D texture, Vector2 position, Action action)
        {
            Texture = texture;
            Position = position;
            clickAction = action;
        }

        public bool isClicked(Vector2 input)
        {
            if (Position.X +Texture.Width >= input.X && Position.X - Texture.Width <= input.X
                &&  Position.Y + Texture.Height >= input.Y && Position.Y - Texture.Height  <= input.Y)
                return true;
            return false;
        }

        public void Click()
        {
            clickAction.Invoke();
        }
    }
}