using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Project3
{
    class Screen
    {
        public List<Button> Buttons = new List<Button>();
        internal void Draw(ref SpriteBatch spriteBatch)
        {
            foreach (Button button in Buttons)
                button.Draw(ref spriteBatch);
        }

        internal void Update(TouchCollection touch)
        {
            if(touch.Count > 0)
            Buttons.ForEach((Button button) =>
            {
                if (button.isClicked(touch[0].Position))
                {
                    button.Click();
                }
            });
        }
    }
}