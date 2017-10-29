using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;
using System.Collections.Generic;

namespace Project3
{
    class Screen
    {
        public List<Button> Buttons = new List<Button>();
        internal void Draw(SpriteBatch spriteBatch)
        {
            Buttons.ForEach((Button button) =>
            {
                spriteBatch.Draw(button.Texture, button.Position, null, Color.White, 0f, Vector2.Zero, Game1.scale, SpriteEffects.None, 0f);
            });
        }

        internal void Update(TouchLocation touch)
        {
            Buttons.ForEach((Button button) =>
            {
                if (button.isClicked(touch.Position))
                {
                    button.Click();
                }
            });
        }
    }
}