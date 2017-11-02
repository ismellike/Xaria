using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project3.Screens
{
    class End : Screen
    {
        public End(ContentManager Content)
        {
            //Texture2D endTexture = Content.Load<Texture2D>("end");
            //Buttons.Add(new Button(startTexture, new Vector2((Game1.screenSize.X - startTexture.Width) / 2f, (Game1.screenSize.Y - startTexture.Height) / 2f), GoToStart));
        }

        void GoToStart()
        {
            Game1.state = GameState.Start;
        }
    }
}