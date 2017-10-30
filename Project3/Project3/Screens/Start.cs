using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project3.Screens
{
    class Start : Screen
    {
        public Start(ContentManager Content)
        {
            Texture2D startTexture = Content.Load<Texture2D>("start");
            Buttons.Add(new Button(startTexture, new Vector2((Game1.screenSize.X-startTexture.Width)/2f, (Game1.screenSize.Y-startTexture.Height)/2f), StartGame));
        }

        void StartGame()
        {
            Game1.state = GameState.Playing;
            
        }
    }
}